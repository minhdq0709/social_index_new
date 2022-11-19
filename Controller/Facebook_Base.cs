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
		private RoundRobinList<AccessTokenFacebook> _roundRobinList;
		private string _path = $"{Environment.CurrentDirectory}/Check/";
		private Dictionary<int, AccessTokenFacebook> _mapTokenSaveToHistory = new Dictionary<int, AccessTokenFacebook>();

		public int SetupToken(string statusToken)
		{
			List<AccessTokenFacebook> listToken = new List<AccessTokenFacebook>();
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listToken.AddRange(mysql.SelectToken(statusToken).OrderBy(x => x.Timecount_token));
			}

			foreach (AccessTokenFacebook item in listToken)
			{
				item.Timecount_token = 0;
				item.Timecount_tokendie = 0;
			}

			if (_roundRobinList != null)
			{
				_roundRobinList = null;
			}

			_roundRobinList = new RoundRobinList<AccessTokenFacebook>(listToken);

			return listToken.Count;
		}

		public FB_CommentModel SetvalueComment(Datum2 data)
		{
			return new FB_CommentModel
			{
				id = data.id,
				created_time = data.created_time,
				created_time_ts = (long)Date_Helper.ConvertDateTimeToTimeStamp(DateTime.Parse(data.created_time)),
				message = data.message,
				nameUseComment = data.from?.name ?? null,
				idUserComment = data.from?.id ?? null,
				post_id = data.post_id,
				timeCrawl = DateTime.Now
			};
		}

		public async Task UpdateNumberUseToken(IDictionary<int, AccessTokenFacebook> data)
		{
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				List<AccessTokenFacebook> tokenOld = mysql.SelectToken($"{Config_System.USER_LIVE}");

				foreach (KeyValuePair<int, AccessTokenFacebook> item in data)
				{
					AccessTokenFacebook temp = tokenOld.FirstOrDefault(x => x.Id == item.Key);

					if (temp != null)
					{
						item.Value.Timecount_token += temp.Timecount_token;
						item.Value.Timecount_tokendie += temp.Timecount_tokendie;
					}

					mysql.UpdateNumberUseToken(item.Value);
					await Task.Delay(100);
				}
			}
		}

		public RoundRobinList<AccessTokenFacebook> GetInstanceRoundRobin()
		{
			return _roundRobinList;
		}

		public string GetInstancePathFolder()
		{
			return _path;
		}

		public void SetValueTokenHistory(AccessTokenFacebook data)
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

		public Dictionary<int, AccessTokenFacebook> GetInstanceMapHistoryToken()
		{
			return _mapTokenSaveToHistory;
		}

		public virtual List<Facebook_Post_Base_Model> GetListPost(int start, string platform)
		{
			List<Facebook_Post_Base_Model> listData = new List<Facebook_Post_Base_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listData = mysql.SelectFieldBaseFromTableSiDemandSourcePost(start, platform).OfType<Facebook_Post_Base_Model>().ToList();
			}

			return listData;
		}
	}
}
