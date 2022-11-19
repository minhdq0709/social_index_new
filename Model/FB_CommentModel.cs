using System;

namespace SocialNetwork_New.Model
{
	class FB_CommentModel
	{
		public string created_time { get; set; }
		public string message { get; set; }
		public string id { get; set; }
		public string post_id { get; set; }
		public long created_time_ts { set; get; }
		public long getdate { get; set; }
		public string nameUseComment { set; get; }
		public string idUserComment { set; get; }
		public DateTime timeCrawl { set; get; }
	}
}
