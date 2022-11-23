using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Tiktok_Si_Demand_Source : Tiktok_Base
	{
		private static volatile ConcurrentQueue<SiDemandSource_Model> _myQueue = new ConcurrentQueue<SiDemandSource_Model>();

		public async Task Crawl(byte thread)
		{
			List<Task> listTask = new List<Task>();
			if (SetupToken(Config_System.TIKTOK_135_TOKEN) == 0)
			{
				return;
			}

			int start = 0;

			if (SetupListPost(start) == 0)
			{
				return;
			}

			for (byte i = 0; i < thread; ++i)
			{
				listTask.Add(Run(i));
			}

			await Task.WhenAll(listTask);
		}

		private int SetupListPost(int start)
		{
			List<SiDemandSource_Model> listData = new List<SiDemandSource_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listData = mysql.SelectFromTableSiDemandSource(start, "tiktok")
					.GroupBy(x => x.link)
					.Select(x => x.First())
					.ToList();

				if (listData.Any())
				{
					foreach (SiDemandSource_Model item in listData)
					{
						_myQueue.Enqueue(item);
					}
				}
			}

			return listData.Count;
		}

		private async Task GetVideo(SiDemandSource_Model data)
		{
			HttpClient_Helper client = new HttpClient_Helper();
			string template = @"https://tiktok-api6.p.rapidapi.com/user/videos?username={0}";
			string nameUser = Regex.Match(data.link, @"(?<=tiktok.com/@)[\w\W\d]+").Value;

			string json = await client.GetAsyncDataByRapidAsync(
				string.Format(template, nameUser),
				GetInstanceRoundRobin().Next().Token,
				"tiktok-api6.p.rapidapi.com");

			Tiktok_Rapid_API6_Model.Root listVideo = String_Helper.ToObject<Tiktok_Rapid_API6_Model.Root>(json);
			if(!listVideo?.videos?.Any() ?? true)
			{
				return;
			}

			Kafka_Helper kh = new Kafka_Helper();
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				foreach (Tiktok_Rapid_API6_Model.Video item in listVideo.videos)
				{
					Tiktok_Post_Kafka_Model dataSendKafka = SetContentPost(item);
					await kh.InsertPost(String_Helper.ToJson<Tiktok_Post_Kafka_Model>(dataSendKafka), Config_System.TOPIC_TIKTOK_POST);

					mysql.InsertToTableSi_demand_source_post(dataSendKafka);
				}
			}
		}

		private async Task Run(byte thread)
		{
			SiDemandSource_Model tempData;
			while (_myQueue.TryDequeue(out tempData))
			{
				await GetVideo(tempData);
				await Task.Delay(thread * 1_000);
			}
		}

		private Tiktok_Post_Kafka_Model SetContentPost(Tiktok_Rapid_API6_Model.Video data)
		{
			string template = "https://www.tiktok.com/@{0}/video/{1}";

			return new Tiktok_Post_Kafka_Model
			{
				IdVideo = data?.video_id,
				UserName = data?.author,
				IdUser = data?.author_id,
				UrlUser = $@"https://www.tiktok.com/@{data?.author}",
				Avatar = data?.avatar_thumb,

				Content = data?.description,
				LinkVideo = string.Format(template, data?.author, data?.video_id),

				Likes = data.statistics?.number_of_hearts ?? 0,
				CommentCounts = data.statistics?.number_of_comments ?? 0,
				Shares = data.statistics?.number_of_reposts ?? 0,
				PlayCounts = data.statistics?.number_of_plays ?? 0,

				TimeCreateTimeStamp = double.Parse(data.create_time ?? "1"),
				TimeCreated = Date_Helper.UnixTimeStampToDateTime(double.Parse(data.create_time ?? "1"))
			};
		}
	}
}
