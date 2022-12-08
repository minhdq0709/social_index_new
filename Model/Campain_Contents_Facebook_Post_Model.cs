using System;

namespace SocialNetwork_New.Model
{
	class Campain_Contents_Facebook_Post_Model
	{
		public string Id { set; get; }
		public string PostID { set; get; }
		public string Url { set; get; }
		public DateTime PostDate { set; get; }
		public DateTime LastDate { set; get; }
		public int CountComment { set; get; }
		public int CountLike { set; get; }
		public int CountShare { set; get; }
		public string AuthorId { set; get; }
		public string AuthorName { set; get; }
		public int PlatFromType { set; get; }
		public string Content { set; get; }
		public string Hastag { set; get; }
		public int Reactions { set; get; }
		public int Si_status { set; get; }
	}
}
