namespace SocialNetwork_New.Model
{
	class Si_Fb_Crawl_Comment_Model : Facebook_Post_Base_Model
	{
		public int status { set; get; }
		public int type { set; get; }

		public int total_comment { set; get; }
	}
}
