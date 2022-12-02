using RoundRobin;
using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Facebook_Base
	{
		private string _path = $"{Environment.CurrentDirectory}/Check/";
		private Dictionary<int, AccessTokenFacebook> _mapTokenSaveToHistory;
		private RoundRobin_Helper<AccessTokenFacebook> _roundRobinList;

		public Facebook_Base()
		{
			_mapTokenSaveToHistory = new Dictionary<int, AccessTokenFacebook>();
			_roundRobinList = new RoundRobin_Helper<AccessTokenFacebook>();
		}

		protected int SetupToken(string statusToken)
		{
			List<AccessTokenFacebook> listToken = new List<AccessTokenFacebook>();
			int len = 0;

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				listToken.AddRange(mysql.SelectToken(statusToken).OrderBy(x => x.Timecount_token));
			}

			foreach (AccessTokenFacebook item in listToken)
			{
				item.Timecount_token = 0;
				item.Timecount_tokendie = 0;

				++len;
			}

			_roundRobinList.SetUp(listToken);

			listToken.Clear();
			listToken.TrimExcess();

			return len;
		}

		protected FB_CommentModel SetValueComment(Datum2 data, string postId)
		{
			return new FB_CommentModel
			{
				id = data.id,
				created_time = data.created_time,
				created_time_ts = (long)Date_Helper.ConvertDateTimeToTimeStamp(DateTime.Parse(data.created_time)),
				message = data.message,
				nameUseComment = data.from?.name ?? null,
				idUserComment = data.from?.id ?? null,
				post_id = postId,
				timeCrawl = DateTime.Now
			};
		}

		protected async Task UpdateNumberUseToken(IDictionary<int, AccessTokenFacebook> data)
		{
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_FB_2_207 : Config_System.DB_FB_51_79))
			{
				IEnumerable<AccessTokenFacebook> tokenOld = mysql.SelectToken($"{Config_System.USER_LIVE}");

				foreach (KeyValuePair<int, AccessTokenFacebook> item in data)
				{
					AccessTokenFacebook temp = tokenOld.FirstOrDefault(x => x.Id == item.Key);

					if (temp != null)
					{
						temp.Timecount_token += item.Value.Timecount_token;
						temp.Timecount_tokendie += item.Value.Timecount_tokendie;

						mysql.UpdateNumberUseToken(temp);
					}

					await Task.Delay(100);
				}
			}
		}

		protected RoundRobin_Helper<AccessTokenFacebook> GetInstanceRoundRobin()
		{
			return _roundRobinList;
		}

		protected string GetInstancePathFolder()
		{
			return _path;
		}

		protected void SetValueTokenHistory(AccessTokenFacebook data)
		{
			if (data.StatusToken == Config_System.USER_LIVE)
			{
				data.Timecount_token++;
			}
			else
			{
				data.Timecount_tokendie++;
			}

			AccessTokenFacebook temp;
			if (_mapTokenSaveToHistory.TryGetValue(data.Id, out temp))
			{
				temp.Timecount_token = data.Timecount_token;
				temp.Timecount_tokendie = data.Timecount_tokendie;
				temp.StatusToken = data.StatusToken;
			}
			else
			{
				_mapTokenSaveToHistory.Add(data.Id, data);
			}
		}

		protected Dictionary<int, AccessTokenFacebook> GetInstanceMapHistoryToken()
		{
			return _mapTokenSaveToHistory;
		}

		protected virtual List<Facebook_Post_Base_Model> GetListPost(int start, string platform)
		{
			List<Facebook_Post_Base_Model> listData = new List<Facebook_Post_Base_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.ON_SEVER == 1 ? Config_System.DB_SOCIAL_INDEX_V2_2_207 : Config_System.DB_SOCIAL_INDEX_V2_51_79))
			{
				listData = mysql.SelectFieldBaseFromTableSiDemandSourcePost(start, platform).OfType<Facebook_Post_Base_Model>().ToList();
			}

			return listData;
		}
	}
}
