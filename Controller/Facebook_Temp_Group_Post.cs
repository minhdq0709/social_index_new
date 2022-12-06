using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Facebook_Temp_Group_Post: Facebook_Base
	{
		private volatile ConcurrentQueue<Temp_Group_Post_Model> _myQueue = new ConcurrentQueue<Temp_Group_Post_Model>();

		public async Task Crawl(byte totalThread, byte isUpdateStatusTokenToDb)
		{
			#region Setup list post
			int start = 0;
			string pathFile = $"{GetInstancePathFolder()}/start.txt";
			DateTime dateCrawl = DateTime.Today.AddDays(-1);

			if (File.Exists(pathFile))
			{
				string text = File.ReadAllText(pathFile);
				start = int.Parse(text);
			}


			if (SetupPostToQueue(start, dateCrawl) == 0)
			{
				start = 0;
				File.WriteAllText(pathFile, $"{start}");

				return;
			}

			File.WriteAllText(pathFile, $"{start + 200}");
			#endregion

			#region Setup token
			if(SetupToken($"{Config_System.USER_LIVE}") == 0)
			{
				return;
			}
			#endregion

			#region Crawl
			List<Task> listTask = new List<Task>();
			for(byte i = 1; i <= totalThread; ++i)
			{
				listTask.Add(Run(i));
			}

			await Task.WhenAll(listTask);
			#endregion

			#region Update time count token
			await UpdateNumberUseToken(GetInstanceMapHistoryToken());
			#endregion

			#region Update status token to db
			if (isUpdateStatusTokenToDb == 1)
			{
				UpdateStatusToken(GetInstanceMapHistoryToken());
			}
			#endregion

			#region Clear memory
			GetInstanceMapHistoryToken().Clear();
			#endregion
		}

		private int SetupPostToQueue(int start, DateTime dateCrawl)
		{
			List<Temp_Group_Post_Model> listData = GetListPost(start, dateCrawl);
			int len = 0;

			if (listData.Any())
			{
				foreach (Temp_Group_Post_Model item in listData)
				{
					_myQueue.Enqueue(item);
					++len;
				}
			}

			listData.Clear();
			listData.TrimExcess();

			return len;
		}

		private List<Temp_Group_Post_Model> GetListPost(int start, DateTime dateCrawl)
		{
			List<Temp_Group_Post_Model> listData = new List<Temp_Group_Post_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listData = mysql.SelectTemp_Group_Post(start, dateCrawl);
			}

			return listData;
		}

		private async Task Run(byte indexThread)
		{
			Temp_Group_Post_Model infoPost;
			while (_myQueue.TryDequeue(out infoPost))
			{
				try
				{
					await GetComment(infoPost);
				}
				catch (Exception) { }

				await Task.Delay(indexThread * 1_000);
			}
		}

		private async Task GetComment(Temp_Group_Post_Model data)
		{
			string afterToken = "";
			ushort limit = 1000;
			long since = (long)Date_Helper.ConvertDateTimeToTimeStamp(data.Createdtime);
			Kafka_Helper kh = new Kafka_Helper(Config_System.SERVER_LINK);
			My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79);
			HttpClient_Helper client = new HttpClient_Helper();

			while (true)
			{
				AccessTokenFacebook tempToken = GetInstanceRoundRobin().Next();

				string url = $"https://graph.facebook.com/v14.0/{data.IdPost}/comments?limit={limit}" +
					$"&access_token={tempToken.Token}" +
					$"&order=reverse_chronological" +
					$"&since={since}" +
					$"&after={afterToken}";

				string json = await client.GetAsyncDataAsync(url);
				await Task.Delay(40_000);

				if (string.IsNullOrEmpty(json))
				{
					Console.WriteLine("rong");
					break;
				}

				if (json.Equals("Sorry, this content isn't available right now"))
				{
					tempToken.StatusToken = Config_System.USER_DIE;
					mysql.InsertToTableFb_Token_History(tempToken);

					SetValueTokenHistory(tempToken);
					break;
				}

				FacebookModel root = String_Helper.ToObject<FacebookModel>(json);
				if (root == null)
				{
					break;
				}

				#region Handle error message
				bool checkHasError = false;
				if (root.error != null)
				{
					checkHasError = true;

					if (root.error.message.Contains("The user must be an administrator"))
					{
						tempToken.StatusToken = Config_System.PAGE_INVALIDATE;
					}

					else if (root.error.message.Contains("You cannot access the app till you log in"))
					{
						tempToken.StatusToken = Config_System.USER_DIE;
					}

					else if (root.error.message.Contains("Error validating access token:"))
					{
						tempToken.StatusToken = Config_System.GET_TOKEN_BACK;
					}

					else if (root.error.message.Equals("Error loading application"))
					{
						tempToken.StatusToken = Config_System.GET_TOKEN_BACK;
					}

					else if (root.error.message.Contains("Please reduce the amount"))
					{
						limit = 100;
						continue;
					}
				}
				#endregion

				#region Set value and send data to kafka
				SetValueTokenHistory(tempToken);
				mysql.InsertToTableFb_Token_History(tempToken);

				if (checkHasError)
				{
					break;
				}

				if (!root.data?.Any() ?? true)
				{
					break;
				}

				foreach (Datum2 item in root.data)
				{
					if (!string.IsNullOrEmpty(item.message))
					{
						await kh.InsertPost(
							String_Helper.ToJson<FB_CommentModel>(SetValueComment(item, data.IdPost)),
							Config_System.TOPIC_FB_GROUP_COMMENT
						);

						++data.Total_Comment;
					}
				}

				kh.Flush(TimeSpan.FromSeconds(10));
				#endregion

				if (String.IsNullOrEmpty(root.paging?.next))
				{
					break;
				}

				afterToken = Regex.Match(root.paging.next, @"(?<=&after=)[\d\w]+").Value;
				if (string.IsNullOrEmpty(afterToken))
				{
					break;
				}
			}

			kh.Dispose();

			/* Update to db */
			mysql.InsertToTableHistoryFbPost(data);

			mysql.Dispose();
		}
	}
}
