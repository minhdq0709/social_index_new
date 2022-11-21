using System;

namespace SocialNetwork_New.Model
{
	class TweetModel
	{
		public long Id { set; get; }
		public string NameUserPost { set; get; }
		public string LinkUserPostTweet { set; get; }
		public string LinkAvatarUserPost { set; get; }
		public DateTime TimePost { set; get; }
		public double TimePostTimeStamp { set; get; }
		public string ContentTweet { set; get; }
		public string UrlTweet { set; get; }
		public string LinkImageOrVideo { set; get; }
		public long Likes { set; get; }
		public long TotalComment { set; get; }
		public long Followers { set; get; }
		public long Following { set; get; }
	}
}
