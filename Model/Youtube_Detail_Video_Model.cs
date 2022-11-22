using System;

namespace SocialNetwork_New.Model
{
	class Youtube_Detail_Video_Model
	{
		public uint Id { set; get; }
		public string VideoCode { get; set; }
		public uint ChannelId { get; set; }
		public string ChannelCode { set; get; }
		public DateTime CreateTime { set; get; }
		public string Duration { set; get; }
		public string Url { set; get; }
		public string Title { set; get; }
		public string ViewCount { set; get; }
		public string LikeCount { set; get; }
		public string FavoriteCount { set; get; }
		public string CommentCount { set; get; }
		public string Author { set; get; }
		public string Thumbnails { set; get; }
		public string Description { set; get; }
	}
}
