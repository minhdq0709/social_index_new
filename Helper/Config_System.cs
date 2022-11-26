namespace SocialNetwork_New.Helper
{
	static class Config_System
	{
		public static readonly string TOPIC_TIKTOK_POST = "crawler-data-tiktok";
		public static readonly string TOPIC_TIKTOK_COMMENT = "crawler-data-tiktok-comment";
		public static readonly string TOPIC_FB_CAMPAIGN = "fb-campaign-crawler";
		public static readonly string TOPIC_TWITTER = "datacollection-twitter-crawler";
		public static readonly string TOPIC_FB_GROUP_COMMENT = "fb-group-cmt-crawler";
		public static readonly string TOPIC_COMMENT_YT = "datacollection-youtubecomment-crawler";
		public static readonly string TOPIC_VIDEO_YT = "datacollection-youtube-crawler";
		public static readonly string TEST = "crawler-fb-group-cmt";

		public static readonly string SERVER_LINK = "10.3.48.81:9092,10.3.48.90:9092,10.3.48.91:9092";
		public static readonly string SERVER_LINK_TEST = "10.5.38.129:9092, 10.5.38.136:9092, 10.5.38.80:9092, 10.5.37.137:9092, 10.5.38.126:9092, 10.5.37.161:9092, 10.5.38.245:9092, 10.5.38.212:9092, 10.5.37.136:9092, 10.5.36.202:9092";
		
		public static readonly int USER_DIE = 100;
		public static readonly int GET_TOKEN_BACK = 101;
		public static readonly int USER_DIED_FOREVER = 102;
		public static readonly int PAGE_INVALIDATE = 105;
		public static readonly int USER_LIVE = 1;

		public static readonly string PROXY_DEFAULT_IP = "http://10.3.51.70:6210";
		public static readonly string PROXY_RANDOM_IP = "http://10.5.3.24:6210";

		/* Server */
		public static readonly string DB_SOCIAL_INDEX_V2_51_79 = "Server=192.168.23.22;User ID=social_index_v2;Password=kSPQ6INMge4p3VFn1bCX;charset=utf8;PORT=3306";
		public static readonly string DB_FB_51_79 = "Server=192.168.23.22;User ID=Campain_PostLinks_Facebook_Db;Password=UgPTNbOSDtCfj0zdmGyH;charset=utf8;PORT=3306";
		public static readonly string DB_SOCIAL_INDEX_V2_2_207 = "Server=192.168.23.22;User ID=social_index_v2_2_207;Password=h5T8jPiwF72kQfPenka7;charset=utf8;PORT=3306";
		public static readonly string DB_FB_2_207 = "Server=192.168.23.22;User ID=FacebookDb_2_207;Password=h5T8jPiwF72kQfPenka7;charset=utf8;PORT=3306";

		/* Local */
		//public static readonly string DB_SOCIAL_INDEX_V2_51_79 = "Server=192.168.23.22;User ID=minhdq;Password=wgy2FdMt0rXfcmCWGSqa;charset=utf8;PORT=3306";
		//public static readonly string DB_FB_51_79 = "Server=192.168.23.22;User ID=minhdq;Password=wgy2FdMt0rXfcmCWGSqa;charset=utf8;PORT=3306";

		/* Telegram */
		public static readonly string KEY_BOT = "5119893484:AAGZbx3izF7B8ywoDmc5gIaAwYRw26doAQ4";
		public static readonly string KEY_BOT_TIKTOK = "5506812278:AAFndGcZgcLwXxxnRwTpKaONdpO7iWv4x6c";
		public static readonly string KEY_ALERT_ACCOUNT_FB_DIE = "5151385536:AAGlVfEU0OqsP3KEQqN_BaSDyj1zyQAHxfU";
		public static readonly string KEY_BOT_YT_COMMENT = "5120269846:AAHzey5xX8ucf4MDe4q5o45m_0gKeaC-8_0";
		public static readonly string KEY_BOT_YT_CHANNEL = "5582300519:AAF1hBFefpLrYWDYjBsx4N9YUXRFscPApHc";
		public static readonly long ID_GROUP_COMMENT_YT = 966893697;
		public static readonly long ID_GROUP_CRAWL_DATA_CHECK_LIVE_ACC = -783988807;

		/* Type token */
		public static readonly byte YOUTUBE_TOKEN = 1;
		public static readonly byte TIKTOK_135_TOKEN = 2;
		public static readonly byte TWITTER_TOKEN = 3;

		/* Status crawl */
		public static readonly sbyte DONE = 2;
		public static readonly sbyte NEW_DATA = 0;
		public static readonly sbyte ERROR = -1;
		public static readonly sbyte HANDLING = 1;
	}
}
