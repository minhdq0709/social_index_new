using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Youtube_Si_Demand_Source_Post : Youtube_Base
	{
		private static volatile ConcurrentQueue<SiDemandSourcePost_Model> _myQueue = new ConcurrentQueue<SiDemandSourcePost_Model>();

		public async Task Crawl(byte totalThread)
		{
			#region Setup list source
			int start = 0;
			string pathFile = $"{GetInstancePathFolder()}/start.txt";

			if (File.Exists(pathFile))
			{
				string text = File.ReadAllText(pathFile);
				start = int.Parse(text);
			}

			if (SetupListPost(0) == 0)
			{
				File.WriteAllText(pathFile, "0");
				return;
			}

			File.WriteAllText(pathFile, $"{start + 200}");
			#endregion

			#region Setup token
			if (SetupToken(Config_System.YOUTUBE_TOKEN) == 0)
			{
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
		}

		private int SetupListPost(int start)
		{
			List<SiDemandSourcePost_Model> listData = new List<SiDemandSourcePost_Model>();
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				listData = mysql.SelectFromTableSiDemandSourcePost(start, "youtube");
			}

			if (listData.Any())
			{
				foreach (SiDemandSourcePost_Model item in listData)
				{
					_myQueue.Enqueue(item);
				}
			}

			return listData.Count;
		}

		private async Task GetComment(SiDemandSourcePost_Model data)
		{
			if (string.IsNullOrEmpty(data.post_id))
			{
				return;
			}

			string pageToken = "";
			Kafka_Helper kh = new Kafka_Helper();
			Telegram_Helper tl = new Telegram_Helper(Config_System.KEY_BOT_YT_COMMENT);

			while (true)
			{
				Youtube_Model obj = await GetCommentVideo(
					data.post_id,
					GetInstanceRoundRobin().Next().Token,
					pageToken);

				if (obj is null)
				{
					break;
				}

				List<Youtube_Comment_Kafka_Model> listCmt = GetCommentDetail(obj.items);
				foreach (Youtube_Comment_Kafka_Model item in listCmt)
				{
					/* Send to kafka */
					await kh.InsertPost(
						String_Helper.ToJson<Youtube_Comment_Kafka_Model>(item),
						Config_System.TOPIC_COMMENT_YT);
				}

				/* Notify to telegram */
				string msg = $"Done {listCmt.Count} comment + reply from video {data.post_id}";
				await tl.SendMessageToChannel(msg, Config_System.ID_GROUP_COMMENT_YT);

				if (String.IsNullOrEmpty(obj.nextPageToken))
				{
					pageToken = string.Empty;
					break;
				}

				pageToken = obj.nextPageToken;
			}
		}

		private async Task Run(byte thread)
		{
			SiDemandSourcePost_Model temp;
			using(My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				while (_myQueue.TryDequeue(out temp))
				{
					await GetComment(temp);
					await Task.Delay(thread * 1_000);

					temp.status = Config_System.DONE;
					mysql.UpdateTimeAndStatusAndUsernameToTableSiDemandSourcePost(temp);
				}
			}
		}

		private List<Youtube_Comment_Kafka_Model> GetCommentDetail(List<Youtube_Model.Item> lstItem)
		{
			List<Youtube_Comment_Kafka_Model> lstData = new List<Youtube_Comment_Kafka_Model>();
			if (lstItem?.Any() ?? false)
			{
				foreach (Youtube_Model.Item item in lstItem)
				{
					if (item.snippet?.topLevelComment?.snippet != null)
					{
						Youtube_Model.Snippet snp = item.snippet.topLevelComment.snippet;
						lstData.Add(SetCommentVideo(snp));

						/* Reply */
						lstData.AddRange(ReplyDetail(item?.replies?.comments));
					}
				}
			}

			return lstData;
		}

		private List<Youtube_Comment_Kafka_Model> ReplyDetail(List<Youtube_Model.TopLevelComment> lstItem)
		{
			List<Youtube_Comment_Kafka_Model> lstData = new List<Youtube_Comment_Kafka_Model>();
			if (lstItem?.Any() ?? false)
			{
				foreach (Youtube_Model.TopLevelComment item in lstItem)
				{
					lstData.Add(SetCommentVideo(item.snippet));
				}
			}

			return lstData;
		}
	}
}
