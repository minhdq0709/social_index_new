using Newtonsoft.Json;
using RoundRobin;
using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SocialNetwork_New.Controller
{
	class Twitter
	{
		public RoundRobinList<Token_Yt_Tiktok_TwitterModel> _roundRobinList;
		private static readonly string[] _nameUser = {
			"YouTube", "TheEllenShow", "TEDTalks", "taylorswift13", "shawnmendes",
			"rihanna", "nytimes", "neymarjr", "NASA", "narendramodi",
			"ladygaga", "KimKardashian", "katyperry", "jtimberlake", "jimmyfallon",
			"elonmusk", "CNN", "britneyspears", "BillGates", "BBCBreaking"
		};

		private static readonly string[] _idUser = {
			"10228272", "15846407", "15492359", "17919972", "379408088",
			"79293791", "807095", "158487331", "11348282", "18839785",
			"14230524", "25365536", "21447363", "26565946", "15485441",
			"44196397", "759251", "16409683", "50393960", "5402612"
		};

		public async Task<int> CrawlData()
		{
			int totalTweet = 0;
			byte count = (byte)_idUser.Count();

			if (SetupToken(Config_System.TWITTER_TOKEN) == 0)
			{
				return totalTweet;
			}

			for (byte i = 0; i < count; ++i)
			{
				totalTweet += await GetDatailTweet(i);
				await Task.Delay(40_000);
			}

			return totalTweet;
		}
		private async ValueTask<int> GetDatailTweet(byte indexItem, int timeout = 60)
		{
			int totalTweet = 0;
			HttpClient client = new HttpClient();
			Kafka_Helper kf = new Kafka_Helper();
			string jsonBody = "";

			using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeout)))
			{
				using (HttpRequestMessage request = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri("https://twitter135.p.rapidapi.com/UserTweets/?count=100&id=" + _idUser[indexItem]),
					Headers = {
						{ "X-RapidAPI-Host", "twitter135.p.rapidapi.com" },
						{ "X-RapidAPI-Key", _roundRobinList.Next().Token}
					}
				})
				{
					try
					{
						using (HttpResponseMessage response = await client.SendAsync(request, cancellationTokenSource.Token))
						{
							response.EnsureSuccessStatusCode();
							jsonBody = await response.Content.ReadAsStringAsync();
						}
					}
					catch (HttpRequestException ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
			}

			if (!string.IsNullOrEmpty(jsonBody))
			{
				XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonBody);
				XmlNodeList lstNode = doc.SelectNodes("//tweet_results/result");

				if (lstNode != null)
				{
					foreach (XmlNode item in lstNode)
					{
						TweetModel objSave = new TweetModel();
						objSave.TimePost = ParseDate(item.SelectSingleNode("./legacy/created_at")?.InnerText);

						/* Get only tweet in today */
						if (objSave.TimePost.Year != 1 && objSave.TimePost.Date == DateTime.Now.Date)
						{
							objSave.Id = String_Helper.convertTextToNumber(item.SelectSingleNode("./rest_id")?.InnerText);

							/* Ignore case users delete their own posts */
							if (objSave.Id == 0)
							{
								continue;
							}

							objSave.NameUserPost = item.SelectSingleNode("./core/user_results/result/legacy/name")?.InnerText;
							objSave.LinkUserPostTweet = @"https://twitter.com/" + _nameUser[indexItem];
							objSave.UrlTweet = objSave.LinkUserPostTweet + "/status/" + objSave.Id.ToString();
							objSave.LinkAvatarUserPost = item.SelectSingleNode("//core/user_results/result/legacy/profile_image_url_https")?.InnerText;
							objSave.TimePostTimeStamp = Date_Helper.ConvertDateTimeToTimeStamp(objSave.TimePost);
							objSave.ContentTweet = item.SelectSingleNode("./legacy/full_text")?.InnerText;
							objSave.LinkImageOrVideo = item.SelectSingleNode("./legacy/extended_entities/media/media_url_https")?.InnerText;

							objSave.Likes = String_Helper.convertTextToNumber(item.SelectSingleNode("./legacy/favorite_count")?.InnerText);
							objSave.TotalComment = String_Helper.convertTextToNumber(item.SelectSingleNode("./legacy/reply_count")?.InnerText);
							objSave.Followers = String_Helper.convertTextToNumber(item.SelectSingleNode("./core/user_results/result/legacy/followers_count")?.InnerText);
							objSave.Following = String_Helper.convertTextToNumber(item.SelectSingleNode("./core/user_results/result/legacy/friends_count")?.InnerText);

							string jsonObj = System.Text.Json.JsonSerializer.Serialize<TweetModel>(objSave, String_Helper.opt);
							await kf.InsertPost(jsonObj, Config_System.TOPIC_TWITTER);

							++totalTweet;
						}
					}
				}
			}

			return totalTweet;
		}

		private DateTime ParseDate(string strDate)
		{
			try
			{
				/* Convert UTC to local time */
				return DateTime.ParseExact(strDate, "ddd MMM dd HH:mm:ss '+0000' yyyy", CultureInfo.InvariantCulture).AddHours(7);
			}
			catch (Exception)
			{
				return new DateTime();
			}
		}

		private int SetupToken(byte type)
		{
			List<Token_Yt_Tiktok_TwitterModel> listToken = new List<Token_Yt_Tiktok_TwitterModel>();
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listToken = mysql.SelectTokenYT_Tiktok_Twitetr(type);
			}

			if (_roundRobinList != null)
			{
				_roundRobinList = null;
			}

			_roundRobinList = new RoundRobinList<Token_Yt_Tiktok_TwitterModel>(listToken);

			return listToken.Count;
		}
	}
}
