using RoundRobin;
using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Youtube_Base
	{
		private RoundRobinList<Token_Yt_Tiktok_TwitterModel> _roundRobinList;
		private string _path = $"{Environment.CurrentDirectory}/Check/";

		public int SetupToken(byte type)
		{
			List<Token_Yt_Tiktok_TwitterModel> listToken = new List<Token_Yt_Tiktok_TwitterModel>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listToken = mysql.SelectTokenYT_Tiktok_Twitetr(Config_System.YOUTUBE_TOKEN);
			}

			if (listToken.Any())
			{
				if (_roundRobinList != null)
				{
					_roundRobinList = null;
				}

				_roundRobinList = new RoundRobinList<Token_Yt_Tiktok_TwitterModel>(listToken);
			}

			return listToken.Count;
		}

		public Youtube_Detail_Video_Kafka_Model SetDetailVideo(Youtube_Model.Item video)
		{
			return new Youtube_Detail_Video_Kafka_Model
			{
				VideoCode = video.id.ToString(),
				ChannelId = 0,
				ChannelCode = video.snippet.channelId,
				CreateTime = video.snippet.publishedAt,
				Duration = null,

				Url = $@"https://www.youtube.com/watch?v={video.id.ToString()}",
				Title = video.snippet.title,
				Description = video.snippet.description,
				Thumbnails = video.snippet.thumbnails?.@default?.url,
				Author = video.snippet.channelTitle,

				LikeCount = video?.statistics?.likeCount ?? "0",
				ViewCount = video?.statistics?.viewCount ?? "0",
				FavoriteCount = video?.statistics?.favoriteCount ?? "0",
				CommentCount = video?.statistics?.commentCount ?? "0"
			};
		}

		public Youtube_Comment_Kafka_Model SetCommentVideo(Youtube_Model.Snippet snp)
		{
			return new Youtube_Comment_Kafka_Model
			{
				Author = snp.authorDisplayName,
				AuthorChannelId = snp.authorChannelId.value,
				Photo = snp.authorProfileImageUrl,
				ChannelId = snp.channelId,

				VideoId = snp.videoId,
				Content = snp.textOriginal,
				Url = $@"https://www.youtube.com/watch?v={snp.videoId}",
				Like = snp.likeCount,

				CreateTime = snp.publishedAt,
				UpdateTime = snp.updatedAt
			};
		}

		public async Task<Youtube_Model> GetCommentVideo(string idVideo, string token, string pageToken)
		{
			string template = "https://www.googleapis.com/youtube/v3/commentThreads?key={0}" +
				"&textFormat=plainText" +
				"&part=snippet,replies" +
				"&videoId={1}" +
				"&maxResults=100" +
				"&mine=true" +
				"&pageToken={2}" +
				"&order=time";

			HttpClient_Helper client = new HttpClient_Helper();
			string json = await client.GetAsyncDataAsync(
				string.Format(template, token, idVideo, pageToken),
				false);

			return String_Helper.ToObject<Youtube_Model>(json);
		}

		public async Task<Youtube_Model> GetDetailIn4Video(string idVideo, string token)
		{
			string template = @"https://www.googleapis.com/youtube/v3/videos?part=snippet&id={0}&key={1}";
			HttpClient_Helper client = new HttpClient_Helper();

			string json = await client.GetAsyncDataAsync(
				string.Format(template, idVideo, token),
				false);

			return String_Helper.ToObject<Youtube_Model>(json);
		}

		public async Task<Youtube_Model> GetReactionVideo(string idVideo, string token)
		{
			string template = @"https://www.googleapis.com/youtube/v3/videos?id={0}&key={1}&part=statistics";
			HttpClient_Helper client = new HttpClient_Helper();

			string json = await client.GetAsyncDataAsync(
				string.Format(template, idVideo, token),
				false);

			return String_Helper.ToObject<Youtube_Model>(json);
		}

		public RoundRobinList<Token_Yt_Tiktok_TwitterModel> GetInstanceRoundRobin()
		{
			return _roundRobinList;
		}

		public string GetInstancePathFolder()
		{
			return _path;
		}
	}
}
