using System;

namespace SocialNetwork_New.Model
{
	class Youtube_Comment_Kafka_Model
	{
		public string Url { set; get; }
		public string Content { set; get; }
		public string Source { set; get; }
		public DateTime CreateTime { set; get; }
		public DateTime UpdateTime { set; get; }
		public string VideoId { set; get; }
		public string Author { set; get; }
		public string AuthorChannelId { set; get; }
		public int Like { set; get; }
		public string Photo { set; get; }
		public string ChannelId { set; get; }
		public int Heart { set; get; }
		public string Tilte { set; get; }
	}
}
