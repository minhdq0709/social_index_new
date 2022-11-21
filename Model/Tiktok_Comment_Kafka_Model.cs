using System;

namespace SocialNetwork_New.Model
{
	class Tiktok_Comment_Kafka_Model
	{
		public string IdPost { set; get; }
		public string IdComment { set; get; }
		public string IdUser { set; get; }
		public string UserComment { set; get; }
		public string UrlUserComment { set; get; }
		public string Content { set; get; }

		public DateTime TimePost { set; get; }
		public long TimePostTimestamp { set; get; }
	}
}
