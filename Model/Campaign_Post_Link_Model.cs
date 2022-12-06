using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_New.Model
{
	class Campaign_Post_Link_Model
	{
		public int Id { set; get; }
		public int CampainId { set; get; }
		public string CampainName { set; get; }
		public int CampainFlatFormTypeId { set; get; }
		public int Campain_SourceLinksId { set; get; }
		public string SourceName { set; get; }
		public string SourceUrl { set; get; }
		public string SourceId { set; get; }
		public string PostId { set; get; }
		public string Link { set; get; }
		public string Subject { set; get; }
		public int IsLock { set; get; }
		public int IsCrawled { set; get; }
		public int TotalCrawled { set; get; }
		public DateTime LastUpdate { set; get; }
		public DateTime PostDate { set; get; }
		public int Comments { set; get; }
		public int Likes { set; get; }
		public int Shares { set; get; }
		public string AuthorId { set; get; }
		public string AuthorName { set; get; }
		public string MessgeStatus { set; get; }
		public int Vote { set; get; }
		public int Ords { set; get; }
		public int CrawlerStatus { set; get; }
		public DateTime LastDateComment { set; get; }
	}
}
