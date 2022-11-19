using System;

namespace SocialNetwork_New.Model
{
	class AccessTokenFacebook
	{
		public int Id { set; get; }
		public string Token { set; get; }
		public int StatusToken { set; get; }
		public string FanPageName { set; get; }
		public string FanPageLink { set; get; }
		public string Manager { set; get; }
		public string User { set; get; }
		public int Timecount_token { set; get; }
		public int Timecount_tokendie { set; get; }
		public DateTime Datetime_tokendie { set; get; }
		public int Token_type { set; get; }
		public string Cookies { set; get; }
		public bool Is_Page_Owner { set; get; }
	}
}
