using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork_New.Helper
{
	class HttpClient_Helper
	{
		public async Task<string> GetAsyncDataAsync(string url, bool isProxyDefaultIp = true,  int timeoutMilis = 20_000)
		{
			using (HttpClientHandler handler = new HttpClientHandler())
			{
				handler.Proxy = new WebProxy(isProxyDefaultIp ? Config_System.PROXY_DEFAULT_IP : Config_System.PROXY_RANDOM_IP);
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

		public async Task<string> GetAsyncDataByRapidAsync(string url, string key, string host, bool isProxyDefaultIp = true, int timeoutMilis = 60_000)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.CancelAfter(timeoutMilis);

			using (HttpClientHandler handler = new HttpClientHandler())
			{
				handler.Proxy = new WebProxy(isProxyDefaultIp ? Config_System.PROXY_DEFAULT_IP : Config_System.PROXY_RANDOM_IP);
				handler.UseProxy = true;

				using (HttpClient client = new HttpClient())
				{
					HttpRequestMessage request = new HttpRequestMessage
					{
						Method = HttpMethod.Get,
						RequestUri = new Uri(url),
						Headers = {
							{ "X-RapidAPI-Key",  key},
							{ "X-RapidAPI-Host", host }
						}
					};

					try
					{
						try
						{
							using (HttpResponseMessage response = await client.SendAsync(request, cancellationTokenSource.Token))
							{
								response.EnsureSuccessStatusCode();
								return await response.Content.ReadAsStringAsync();
							}
						}
						catch (HttpRequestException) { }
					}
					catch (OperationCanceledException) { }
				}

				return string.Empty;
			}
		}
	}
}
