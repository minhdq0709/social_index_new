using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	/// <summary>
	/// Only get comment of video
	/// </summary>
	class Tiktok_Si_Demand_Source_Post : Tiktok_Base
	{
		private static volatile ConcurrentQueue<SiDemandSourcePost_Model> _myQueue = new ConcurrentQueue<SiDemandSourcePost_Model>();

		public async Task Crawl(byte totalThread)
		{
			#region Setup list post
			int start = 0;
			string pathFile = $"{GetInstancePathFolder()}/start.txt";

			if (File.Exists(pathFile))
			{
				string text = File.ReadAllText(pathFile);
				start = int.Parse(text);
			}

			if (SetupListPost(start) == 0)
			{
				start = 0;
				File.WriteAllText(pathFile, $"{start}");

				return;
			}

			File.WriteAllText(pathFile, $"{start + 200}");
			#endregion

			#region Setup token
			if (SetupToken(Config_System.TIKTOK_135_TOKEN) == 0)
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
				listData = mysql.SelectFromTableSiDemandSourcePost(start, "tiktok");
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

			ushort offset = 0;
			Kafka_Helper kf = new Kafka_Helper();
			HttpClient_Helper client = new HttpClient_Helper();
			string template = @"https://tiktok-all-in-one.p.rapidapi.com/video/comments?id={0}&offset={1}";
			data.status = Config_System.DONE;

			while (true)
			{
				await Task.Delay(20_000);

				string json = await client.GetAsyncDataByRapidAsync(
					string.Format(template, data.post_id, offset),
					GetInstanceRoundRobin().Next().Token,
					"tiktok-all-in-one.p.rapidapi.com",
					false);

				if (string.IsNullOrEmpty(json))
				{
					data.status = Config_System.ERROR;
					return;
				}

				Tiktok_Comment_Rapid_Model cmtData = String_Helper.ToObject<Tiktok_Comment_Rapid_Model>(json);
				if ((!cmtData?.comments?.Any() ?? true) || cmtData.status_code != 0)
				{
					return;
				}

				foreach (Comment item in cmtData.comments)
				{
					Tiktok_Comment_Kafka_Model dataKafka = SetContentComment(item);
					if (!string.IsNullOrEmpty(dataKafka.Content))
					{
						await kf.InsertPost(
							String_Helper.ToJson<Tiktok_Comment_Kafka_Model>(dataKafka),
							Config_System.TOPIC_TIKTOK_COMMENT
						);
					}
				}

				if (cmtData.has_more != 1)
				{
					break;
				}

				offset += 20;
			}

			/* Update in4 post to db */
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				mysql.UpdateTimeAndStatusAndUsernameToTableSiDemandSourcePost(data);
			}
		}

		private async Task GetDetailPost(SiDemandSourcePost_Model data)
		{
			if (string.IsNullOrEmpty(data.post_id))
			{
				return;
			}

			HttpClient_Helper client = new HttpClient_Helper();
			string template = @"https://tiktok-all-in-one.p.rapidapi.com/video?id={0}";

			await Task.Delay(20_000);

			string json = await client.GetAsyncDataByRapidAsync(
				string.Format(template, data.post_id),
				GetInstanceRoundRobin().Next().Token,
				"tiktok-all-in-one.p.rapidapi.com",
				false
			);

			if (string.IsNullOrEmpty(json))
			{
				data.status = Config_System.ERROR;
				return;
			}

			Tiktok_Post_Rapid_Model tempData = String_Helper.ToObject<Tiktok_Post_Rapid_Model>(json);
			if (data is null || data.status != 0)
			{
				return;
			}

			Tiktok_Post_Kafka_Model dataToKafka = SetContentPost(tempData);
			Kafka_Helper kh = new Kafka_Helper();

			await kh.InsertPost(
				String_Helper.ToJson<Tiktok_Post_Kafka_Model>(dataToKafka),
				Config_System.TOPIC_TIKTOK_POST
			);
		}

		private async Task Run(byte indexThread)
		{
			SiDemandSourcePost_Model tempData;
			while (_myQueue.TryDequeue(out tempData))
			{
				await GetComment(tempData);
				await Task.Delay(indexThread * 1_000);
			}
		}
	}
}
