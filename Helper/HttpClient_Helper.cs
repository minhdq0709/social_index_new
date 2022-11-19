using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork_New.Helper
{
	class HttpClient_Helper
	{
		public async Task<string> GetAsyncDataAsync(string url, int timeoutMilis = 20_000)
		{
            using (HttpClientHandler handler = new HttpClientHandler())
            {
				handler.Proxy = new WebProxy(Config_System.PROXY_ONLY_ONE_IP);
				handler.UseProxy = true;

				using (HttpClient client = new HttpClient(handler))
                {
                    using (var cts = new CancellationTokenSource(timeoutMilis))
                    {
                        try
                        {
                            using (HttpResponseMessage response = await client.GetAsync(url, cts.Token))
                            {
                                return await response.Content.ReadAsStringAsync();
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }

            return string.Empty;
        }
	}
}
