using System;
using System.Collections.Generic;

namespace SocialNetwork_New.Model
{
	class Youtube_Model
	{
		public class AuthorChannelId
		{
			public string value { get; set; }
		}

		public class Item
		{
			public string kind { get; set; }
			public string etag { get; set; }
			public object id { get; set; }
			public Snippet snippet { get; set; }
			public Replies replies { get; set; }
			public Statistics statistics { get; set; }
		}

		public class Id
		{
			public string kind { get; set; }
			public string channelId { get; set; }

			public string videoId { get; set; }
		}

		public class PageInfo
		{
			public int totalResults { get; set; }
			public int resultsPerPage { get; set; }
		}

		public class Statistics
		{
			public string viewCount { set; get; }
			public string likeCount { set; get; }
			public string favoriteCount { set; get; }
			public string commentCount { set; get; }
			public string videoCount { set; get; }
			public string subscriberCount { set; get; }
		}

		public class Default
		{
			public string url { get; set; }
			public int width { get; set; }
			public int height { get; set; }
		}

		public class High
		{
			public string url { get; set; }
			public int width { get; set; }
			public int height { get; set; }
		}

		public class Medium
		{
			public string url { get; set; }
			public int width { get; set; }
			public int height { get; set; }
		}

		public class Thumbnails
		{
			public Default @default { get; set; }
			public Medium medium { get; set; }
			public High high { get; set; }
		}
		public class Snippet
		{
			public string videoId { get; set; }
			public TopLevelComment topLevelComment { get; set; }
			public bool canReply { get; set; }
			public int totalReplyCount { get; set; }
			public bool isPublic { get; set; }
			public string textDisplay { get; set; }
			public string textOriginal { get; set; }
			public string authorDisplayName { get; set; }
			public string authorProfileImageUrl { get; set; }
			public string authorChannelUrl { get; set; }
			public AuthorChannelId authorChannelId { get; set; }
			public bool canRate { get; set; }
			public string viewerRating { get; set; }
			public int likeCount { get; set; }
			public DateTime publishedAt { get; set; }
			public DateTime updatedAt { get; set; }
			public string description { get; set; }
			public string country { get; set; }
			public string channelId { get; set; }
			public DateTime publishTime { set; get; }
			public string channelTitle { get; set; }

			public string title { get; set; }

			public Thumbnails thumbnails { get; set; }
		}

		public class TopLevelComment
		{
			public string kind { get; set; }
			public string etag { get; set; }
			public string id { get; set; }
			public Snippet snippet { get; set; }
		}

		public string kind { get; set; }
		public string etag { get; set; }
		public string nextPageToken { get; set; }
		public string prevPageToken { set; get; }
		public PageInfo pageInfo { get; set; }

		public class Replies
		{
			public List<TopLevelComment> comments { get; set; }
		}

		public List<Item> items { get; set; }
	}
}
