using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Facebook_Comment : Facebook_Base
	{
		private static volatile ConcurrentQueue<SiDemandSourcePost> _myQueue = new ConcurrentQueue<SiDemandSourcePost>();

		public async Task Crawl()
		{
			#region Setup token
			if (SetupToken($"{Config_System.USER_LIVE}") < 10)
			{
				return;
			}
			#endregion

			#region Setup list post
			string pathFile = $"{GetInstancePathFolder()}/start.txt";
			int start = 0;

			if (File.Exists(pathFile))
			{
				string text = File.ReadAllText(pathFile);
				start = int.Parse(text);
			}

			if (SetupPostToQueue(start) == 0)
			{
				start = 0;
				File.WriteAllText(pathFile, $"{start}");

				return;
			}

			File.WriteAllText(pathFile, $"{start + 200}");
			#endregion

			#region Crawl
			Task t1 = Run(1);
			//Task t2 = Run(2);
			//Task t3 = Run(3);
			//Task t4 = Run(4);
			//Task t5 = Run(5);
			//Task t6 = Run(6);

			await Task.WhenAll(t1); //t2, t3, t4, t5, t6
			#endregion

			#region Update to db - Send msg to kafka
			await UpdateNumberUseToken(GetInstanceMapHistoryToken());
			#endregion

			#region Free memory
			GetInstanceMapHistoryToken().Clear();
			#endregion
		}

		private int SetupPostToQueue(int start)
		{
			List<SiDemandSourcePost> listData = GetListPost(start, "facebook").OfType<SiDemandSourcePost>().ToList();

			foreach (SiDemandSourcePost item in listData)
			{
				_myQueue.Enqueue(item);
			}

			return listData.Count;
		}

		private async Task GetComment(SiDemandSourcePost data)
		{
			string afterToken = "";
			ushort limit = 1000;
			double since = data.update_time.Year == 1 ? 1 : Date_Helper.ConvertDateTimeToTimeStamp(data.update_time);
			Kafka_Helper kh = new Kafka_Helper();
			My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79);
			int totalComment = 0;
			byte loop = 1;
			HttpClient_Helper client = new HttpClient_Helper();
			AccessTokenFacebook tempToken = new AccessTokenFacebook();
			bool checkHasError = false;

			while (true)
			{
				tempToken = GetInstanceRoundRobin().Next();
				checkHasError = false;

				string url = $"https://graph.facebook.com/v14.0/{data.post_id}/comments?limit={limit}" +
					$"&access_token={tempToken.Token}" +
					$"&order=reverse_chronological" +
					$"&since={since}" +
					$"&after={afterToken}";

				string json = await client.GetAsyncDataAsync(url);
				await Task.Delay(40_000);

				if (string.IsNullOrEmpty(json))
				{
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
				if (root.error != null)
				{
					checkHasError = true;

					if (root.error.message.Contains("The user must be an administrator"))
					{
						tempToken.StatusToken = Config_System.PAGE_INVALIDATE;
					}

					if (root.error.message.Contains("You cannot access the app till you log in"))
					{
						tempToken.StatusToken = Config_System.USER_DIE;
					}

					if (root.error.message.Contains("Error validating access token:"))
					{
						tempToken.StatusToken = Config_System.GET_TOKEN_BACK;
					}

					if (root.error.message.Equals("Error loading application"))
					{
						tempToken.StatusToken = Config_System.GET_TOKEN_BACK;
					}

					if (root.error.message.Contains("Please reduce the amount"))
					{
						limit = 100;
						continue;
					}
				}
				#endregion

				#region Set value and send data to kafka
				SetValueTokenHistory(tempToken);
				mysql.InsertToTableFb_Token_History(tempToken);

				if(checkHasError)
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
						item.post_id = data.post_id;

						++totalComment;
						string jsonObj = System.Text.Json.JsonSerializer.Serialize<FB_CommentModel>(SetvalueComment(item), String_Helper.opt);
						await kh.InsertPost(jsonObj, Config_System.TOPIC_FB_GROUP_COMMENT);
					}
				}
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

				++loop;
			}

			data.total_comment = totalComment;
			data.total_share = loop;

			mysql.InsertToTableHistoryFbPost(data);
			mysql.Dispose();
		}

		private async Task Run(byte indexThread)
		{
			SiDemandSourcePost infoPost;
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
	}
}
