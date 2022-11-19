using System;

namespace SocialNetwork_New.Model
{
	class SiDemandSource
	{
		public int id { set; get; }
		public string platform { set; get; }
		public string link { set; get; }
		public int type { set; get; }
		public int priority { set; get; }
		public string frequency { set; get; }
		public int frequency_crawl_current_date { set; get; }
		public string demand { set; get; }
		public DateTime create_time { set; get; }
		public DateTime update_time { set; get; }
		public DateTime update_time_crawl { set; get; }
		public int status { set; get; }
		public string source_id { set; get; }
		public string source_name { set; get; }
		public int lock_tmp { set; get; }
		public string message_error { set; get; }
		public string user_crawler { set; get; }
		public int is_ended { set; get; }
		public int ticket_id { set; get; }
	}
}
