using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Tiktok_Campaign_Source_Link
	{
		private RoundRobin_Helper<Token_Yt_Tiktok_TwitterModel> _roundRobinList;

		public async Task Crawl()
		{
			#region Setup list post
			List<Campain_Source_Links_Model> listData = GetListSource(16);

			if (!listData.Any())
			{
				return;
			}
			#endregion

			#region Setup token
			if(SetupToken(4) == 0)
			{
				return;
			}
			#endregion

			#region Crawl
			foreach(Campain_Source_Links_Model item in listData)
			{
				List<Campain_Contents_Facebook_Post_Model> listPost = await GetDetailPost(item.Name);
				if(listData.Any())
				{
					/* Save data to db */

				}
			}
			#endregion
		}

		private List<Campain_Source_Links_Model> GetListSource(int campaignId)
		{
			List<Campain_Source_Links_Model> listSource = new List<Campain_Source_Links_Model>();
			using(My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listSource = mysql.SelectFromTableCampaignSourceLink(campaignId);
			}

			return listSource;
		}

		private int SetupToken(byte type)
		{
			List<Token_Yt_Tiktok_TwitterModel> listToken = new List<Token_Yt_Tiktok_TwitterModel>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listToken = mysql.SelectTokenYT_Tiktok_Twitetr(type);
			}

			_roundRobinList.SetUp(listToken);

			return listToken.Count();
		}

		private async Task<List<Campain_Contents_Facebook_Post_Model>> GetDetailPost(string userName)
		{
			HttpClient_Helper client = new HttpClient_Helper();
			string json = await client.GetAsyncDataByRapidAsync(
				$"https://tiktok-api6.p.rapidapi.com/user/videos?username={userName}",
				_roundRobinList.Next().Token,
				"tiktok-api6.p.rapidapi.com",
				false
			);

			List<Campain_Contents_Facebook_Post_Model> listData = new List<Campain_Contents_Facebook_Post_Model>();

			if (!string.IsNullOrEmpty(json))
			{
				Tiktok_API6_Rapid_Model.Root data = String_Helper.ToObject<Tiktok_API6_Rapid_Model.Root>(json);

				if(data?.videos?.Any() ?? false)
				{
					foreach (Tiktok_API6_Rapid_Model.Video item in data.videos)
					{
						Campain_Contents_Facebook_Post_Model fb = new Campain_Contents_Facebook_Post_Model();
						fb.PostID = item.video_id;
						fb.CountComment = item.statistics.number_of_comments;
						fb.CountLike = item.statistics.number_of_hearts;
						fb.CountShare = item.statistics.number_of_reposts;
						fb.AuthorId = item.author_id;
						fb.AuthorName = item.author_name;
						fb.Url = item.download_url;
						fb.Content = item.description;

						listData.Add(fb);
						
					}
				}
			}

			return listData;
		}
	}
}
