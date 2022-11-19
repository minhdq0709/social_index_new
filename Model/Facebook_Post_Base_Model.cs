using System;

namespace SocialNetwork_New.Model
{
	class Facebook_Post_Base_Model
	{
		public int id { set; get; }
		public DateTime create_time { set; get; }
		public DateTime update_time { set; get; }
		public string post_id { set; get; }
	}
}
