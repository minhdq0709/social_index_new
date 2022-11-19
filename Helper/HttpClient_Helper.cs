using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNetwork_New.Helper
{
	class HttpClient_Helper
	{
		private static HttpClientHandler handler = new HttpClientHandler
		{
			Proxy = new WebProxy(Config_System.PROXY_ONLY_ONE_IP),
			UseProxy = true
		};

		public async Task<string> GetAsyncDataAsync(string url)
		{
			try
			{
				using (HttpClient client = new HttpClient(handler))
				{
					return await client.GetStringAsync(url);
				}
			}
			catch (Exception) { }

			return string.Empty;
		}
	}
}
