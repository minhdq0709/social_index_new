using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode.Playlists;

namespace SocialNetwork_New.Controller
{
	class Youtube_Si_Demand_Source : Youtube_Base
	{
		private static volatile ConcurrentQueue<SiDemandSource_Model> _myQueue = new ConcurrentQueue<SiDemandSource_Model>();

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

			if (GetListChannel(0) == 0)
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
			for(byte i = 1; i <= totalThread; ++i)
			{
				listTask.Add(Run(i));
			}

			await Task.WhenAll(listTask);
			#endregion
		}

		private int GetListChannel(int start)
		{
			List<SiDemandSource_Model> listData = new List<SiDemandSource_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_SOCIAL_INDEX_V2_2_207 : Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				listData = mysql.SelectFromTableSiDemandSource(start, "youtube")
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

		private async Task<DateTime> GetVideoFromChannel(SiDemandSource_Model source)
		{
			string idChannel = Regex.Match(source.link, @"(?<=channel/)[\W\w\d]+").Value;
			DateTime lastTimePublishedVideo = source.update_time;

			if (string.IsNullOrEmpty(idChannel))
			{
				return lastTimePublishedVideo;
			}

			var videos = MyTube_Helper.Instance.GetYoutubeClient().Channels
				.GetUploadsAsync(YoutubeExplode.Channels.ChannelId.Parse(idChannel))
				.GetAsyncEnumerator();

			My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_SOCIAL_INDEX_V2_2_207 : Config_System.DB_SOCIAL_INDEX_V2_51_79);
			Kafka_Helper kh = new Kafka_Helper(Config_System.SERVER_LINK);

			while (await videos.MoveNextAsync())
			{
				await Task.Delay(20_000);
				PlaylistVideo video = videos.Current;

				/* Get detail video */
				Youtube_Model detailVideo = await GetDetailIn4Video(video.Id, GetInstanceRoundRobin().Next().Token);
				if(!detailVideo?.items?.Any() ?? true)
				{
					continue;
				}

				if(detailVideo.items[0].snippet.publishedAt < source.update_time)
				{
					break;
				}

				/* Get last date video */
				if(detailVideo.items[0].snippet.publishedAt > lastTimePublishedVideo)
				{
					lastTimePublishedVideo = detailVideo.items[0].snippet.publishedAt;
				}

				/* Set value for model */
				Youtube_Detail_Video_Kafka_Model detailVideoKafka = new Youtube_Detail_Video_Kafka_Model {
					VideoCode = video.Id,
					ChannelId = 0,
					ChannelCode = idChannel,
					CreateTime = detailVideo.items.FirstOrDefault().snippet.publishedAt,
					Duration = video.Duration.ToString(),

					Url = video.Url,
					Title = video.Title,
					Description = detailVideo?.items?.FirstOrDefault()?.snippet?.description,
					Thumbnails = video.Thumbnails?.FirstOrDefault()?.Url ?? "",
					Author = video.Author.ChannelTitle,

					LikeCount = detailVideo?.items?.FirstOrDefault()?.statistics?.likeCount ?? "0",
					ViewCount = detailVideo?.items?.FirstOrDefault()?.statistics?.viewCount ?? "0",
					FavoriteCount = detailVideo?.items?.FirstOrDefault()?.statistics?.favoriteCount ?? "0",
					CommentCount = detailVideo?.items?.FirstOrDefault()?.statistics?.commentCount ?? "0"
				};

				/* Insert to table si_demand_source_post */
				mysql.InsertToTableSi_demand_source_post(detailVideoKafka);

				/* Send to kafka */
				await kh.InsertPost(
					String_Helper.ToJson<Youtube_Detail_Video_Kafka_Model>(detailVideoKafka),
					Config_System.TOPIC_VIDEO_YT);
			}

			/* Update data to table si_demand_source */
			source.frequency_crawl_status_current_date = "done";
			source.user_crawler = "Minhdq";
			source.update_time = DateTime.Now;
			source.update_time_crawl = DateTime.Now;
			source.frequency_crawl_current_date++;
			source.status = Config_System.DONE;

			mysql.UpdateTimeAndStatusAndFrequencyCrawlStatutToTableSiDemandSource(source);

			kh.Dispose();
			mysql.Dispose();

			return lastTimePublishedVideo;
		}

		private async Task Run(byte thread)
		{
			SiDemandSource_Model tempData;
			while (_myQueue.TryDequeue(out tempData))
			{
				await GetVideoFromChannel(tempData);
				await Task.Delay(thread * 1_000);
			}
		}
	}
}
