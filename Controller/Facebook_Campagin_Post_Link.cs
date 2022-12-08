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
	class Facebook_Campagin_Post_Link : Facebook_Base
	{
		private volatile ConcurrentQueue<Campaign_Post_Link_Model> _myQueue = new ConcurrentQueue<Campaign_Post_Link_Model>();

		public async Task Crawl(byte totalThread, byte campaginId, byte type, byte isUpdateStatusTokenToDb)
		{
			#region Setup list post
			if (SetupPostToQueue(campaginId, type) == 0)
			{
				return;
			}
			#endregion

			#region Setup token
			if (SetupToken($"{Config_System.USER_LIVE}") == 0)
			{
				Console.WriteLine("token = 0");
				return;
			}
			#endregion

			#region Crawl
			List<Task> listTask = new List<Task>();
			for (byte i = 1; i <= totalThread; ++i)
			{
				listTask.Add(Run(i));
			}

			await Task.WhenAll(listTask);
			#endregion

			#region Update to db
			await UpdateNumberUseToken(GetInstanceMapHistoryToken());
			#endregion

			#region Update status token to db
			if (type == 1)
			{
				UpdateStatusToken(GetInstanceMapHistoryToken());
			}
			#endregion

			#region Free memory
			GetInstanceMapHistoryToken().Clear();
			#endregion
		}

		private int SetupPostToQueue(byte campaginId, byte type)
		{
			IEnumerable<Campaign_Post_Link_Model> listData = GetListPost(campaginId, type);

			foreach (Campaign_Post_Link_Model item in listData)
			{
				_myQueue.Enqueue(item);
			}

			return listData.Count();
		}

		private IEnumerable<Campaign_Post_Link_Model> GetListPost(byte campaginId, byte type)
		{
			IEnumerable<Campaign_Post_Link_Model> listData;

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listData = mysql.SelectTableCampain_PostLinks(campaginId, type);
			}

			return listData;
		}

		private async Task Run(byte indeThread)
		{
			Campaign_Post_Link_Model temp;
			while (_myQueue.TryDequeue(out temp))
			{
				await GetComment(temp);

				/* Update status is done */
				using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ?
					Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
				{
					temp.CrawlerStatus = Config_System.DONE;
					mysql.UpdateCrawlerStatus_Campain_PostLinks(temp);
				}

				await Task.Delay(indeThread * 1_000);
			}
		}

		private async Task GetComment(Campaign_Post_Link_Model data)
		{
			string afterToken = "";
			ushort limit = 1000;
			double since = Date_Helper.ConvertDateTimeToTimeStamp(data.LastUpdate);
			Kafka_Helper kh = new Kafka_Helper(Config_System.SERVER_LINK);
			My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79);
			HttpClient_Helper client = new HttpClient_Helper();

			while (true)
			{
				await Task.Delay(40_000);

				AccessTokenFacebook tempToken = GetInstanceRoundRobin().Next();

				string url = $"https://graph.facebook.com/v14.0/{data.PostId}/comments?limit={limit}" +
					$"&access_token={tempToken.Token}" +
					$"&order=reverse_chronological" +
					$"&since={since}" +
					$"&after={afterToken}";

				string json = await client.GetAsyncDataAsync(url);

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
							String_Helper.ToJson<FB_CommentModel>(SetValueComment(item, data.PostId)),
							Config_System.TOPIC_FB_GROUP_COMMENT
						);

						++data.Comments;
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
