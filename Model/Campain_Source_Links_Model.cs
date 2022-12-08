using System;

namespace SocialNetwork_New.Model
{
	class Campain_Source_Links_Model
	{
		public int Id { set; get; }
		public int CampainId { set; get; }
		public int LinkId { set; get; }
		public string Url { set; get; }
		public DateTime PostDate { set; get; }
		public DateTime LastUpdate { set; get; }
		public int Vote { set; get; }
		public int IsLock { set; get; }
		public int Ords { set; get; }
		public string Name { set; get; }
		public int CampainFlatFormTypeId { set; get; }
		public string SourceId { set; get; }
		public int TotalCrawled { set; get; }
		public int IsCrawling { set; get; }
		public int TotalPost { set; get; }
		public DateTime PostLastest { set; get; }
		public int IsPrivate { set; get; }
	}
}
