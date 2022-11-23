using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode.Playlists;

namespace SocialNetwork_New.Controller
{
	class Youtube_Si_Demand_Source : Youtube_Base
	{
		private async Task Crawl(byte totalThread)
		{
			await Task.Delay(1);
		}

		private int GetListChannel(int start)
		{
			return 0;
		}

		private async Task GetVideoFromChannel(SiDemandSource_Model source)
		{
			string idChannel = Regex.Match(source.link, @"(?<=channel/)[\W\w\d]+").Value;
			if (string.IsNullOrEmpty(idChannel))
			{
				return;
			}

			var videos = MyTube_Helper.Instance.GetYoutubeClient().Channels
				.GetUploadsAsync(YoutubeExplode.Channels.ChannelId.Parse(idChannel))
				.GetAsyncEnumerator();

			while (await videos.MoveNextAsync())
			{
				await Task.Delay(20_000);
				PlaylistVideo video = videos.Current;

				/* Get detail video */
				Youtube_Model detailVideo = await GetDetailIn4Video(video.Id, GetInstanceRoundRobin().Next().Token);
				if(!detailVideo?.items?.Any() ?? true)
				{
					continue;
				}

				if(detailVideo.items[0].snippet.publishedAt < source.update_time)
				{
					break;
				}


			}
		}
	}
}
