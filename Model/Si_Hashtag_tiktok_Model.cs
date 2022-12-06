using System;

namespace SocialNetwork_New.Model
{
	class Si_Hashtag_tiktok_Model
	{
		public int id { set; get; }
		public string hashtag { set; get; }
		public int status { set; get; }
		public DateTime start_time { set; get; }
		public DateTime end_time { set; get; }
		public DateTime create_time { set; get; }
		public DateTime update_time { set; get; }
		public string hashtag_code { set; get; }
		public int is_frequently { set; get; }
	}
}
