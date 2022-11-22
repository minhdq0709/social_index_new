using System;

namespace SocialNetwork_New.Model
{
	class SiDemandSourcePost_Model: Facebook_Post_Base_Model
	{
		public int si_demand_source_id{ set; get; }
		public string link{ set; get; }
		public int status{ set; get; }
		public string title{ set; get; }
		public string content{ set; get; }
		public int total_comment{ set; get; }
		public int total_like{ set; get; }
		public int total_share{ set; get; }
		public int lock_tmp{ set; get; }
		public string user_crawler{ set; get; }
	}
}
