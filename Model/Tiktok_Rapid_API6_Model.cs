using System.Collections.Generic;

namespace SocialNetwork_New.Model
{
	public class Tiktok_Rapid_API6_Model
	{
		public class Root
		{
			public string username { get; set; }
			public string secondary_id { get; set; }
			public string continuation_token { get; set; }
			public List<Video> videos { get; set; }
		}

		public class Statistics
		{
			public int number_of_comments { get; set; }
			public int number_of_hearts { get; set; }
			public int number_of_plays { get; set; }
			public int number_of_reposts { get; set; }
		}

		public class Video
		{
			public string video_id { get; set; }
			public string description { get; set; }
			public string create_time { get; set; }
			public string author { get; set; }
			public string author_id { get; set; }
			public string author_name { get; set; }
			public Statistics statistics { get; set; }
			public string cover { get; set; }
			public string download_url { get; set; }
			public string video_definition { get; set; }
			public object format { get; set; }
			public int bitrate { get; set; }
			public int duration { get; set; }
			public string avatar_thumb { get; set; }
		}
	}
}
