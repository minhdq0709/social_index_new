using RoundRobin;
using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork_New.Controller
{
	class Tiktok_Base
	{
		private RoundRobinList<Token_Yt_Tiktok_TwitterModel> _roundRobinList;
		private string _path = $"{Environment.CurrentDirectory}/Check/";

		public int SetupToken(byte type)
		{
			List<Token_Yt_Tiktok_TwitterModel> listToken = new List<Token_Yt_Tiktok_TwitterModel>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listToken = mysql.SelectTokenYT_Tiktok_Twitetr(Config_System.TIKTOK_135_TOKEN);
				if (listToken.Any())
				{
					if (_roundRobinList != null)
					{
						_roundRobinList = null;
					}

					_roundRobinList = new RoundRobinList<Token_Yt_Tiktok_TwitterModel>(listToken);
				}
			}

			return listToken.Count;
		}

		public RoundRobinList<Token_Yt_Tiktok_TwitterModel> GetInstanceRoundRobin()
		{
			return _roundRobinList;
		}

		public string GetInstancePathFolder()
		{
			return _path;
		}

		public Tiktok_Post_Kafka_Model SetContentPost(Tiktok_Post_Rapid_Model data)
		{
			double timeCreateVideo = (double)(data.aweme_detail?.create_time);
			string template = "https://www.tiktok.com/@{0}/video/{1}";

			return new Tiktok_Post_Kafka_Model
			{
				IdVideo = data.aweme_detail?.status?.aweme_id ?? "",
				UserName = data.aweme_detail?.author?.nickname,
				IdUser = data.aweme_detail?.author.uid,
				UrlUser = $@"https://www.tiktok.com/@{data.aweme_detail?.author?.unique_id}",
				Avatar = data.aweme_detail?.author?.avatar_168x168?.url_list[0] ?? null,

				Content = data.aweme_detail?.desc,
				LinkVideo = string.Format(template, data.aweme_detail?.author?.unique_id, data.aweme_detail?.status?.aweme_id),

				Likes = data.aweme_detail?.statistics?.digg_count ?? 0,
				CommentCounts = data.aweme_detail?.statistics?.comment_count ?? 0,
				Shares = data.aweme_detail?.statistics?.share_count ?? 0,
				PlayCounts = data.aweme_detail?.statistics?.play_count ?? 0,

				TimeCreateTimeStamp = timeCreateVideo,
				TimeCreated = Date_Helper.UnixTimeStampToDateTime(timeCreateVideo)
			};
		}

		public Tiktok_Comment_Kafka_Model SetContentComment(Comment item)
		{
			return new Tiktok_Comment_Kafka_Model
			{
				IdPost = item.aweme_id,
				IdComment = item.cid,
				UserComment = item.user.nickname,
				IdUser = item.user.uid,
				UrlUserComment = $"https://www.tiktok.com/@{item.user.unique_id}",
				Content = item.text,

				TimePostTimestamp = item.create_time,
				TimePost = Date_Helper.UnixTimeStampToDateTime(item.create_time)
			};
		}
	}
}
