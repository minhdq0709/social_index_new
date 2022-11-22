using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Tiktok_Si_Demand_Source : Tiktok_Base
	{
		private static volatile ConcurrentQueue<SiDemandSource_Model> _myQueue = new ConcurrentQueue<SiDemandSource_Model>();

		public async Task Crawl(byte thread)
		{
			List<Task> listTask = new List<Task>();
			if (SetupToken(Config_System.TIKTOK_135_TOKEN) == 0)
			{
				return;
			}

			await demo();

			int start = 0;

			if (SetupListPost(start) == 0)
			{
				return;
			}

			for (byte i = 0; i < thread; ++i)
			{
				listTask.Add(Run(i));
			}

			await Task.WhenAll(listTask);
		}

		private int SetupListPost(int start)
		{
			List<SiDemandSource_Model> listData = new List<SiDemandSource_Model>();

			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listData = mysql.SelectFromTableSiDemandSource(start, "tiktok");
				if (listData.Any())
				{
					foreach (SiDemandSource_Model item in listData)
					{
						_myQueue.Enqueue(item);
					}
				}
			}

			return listData.Count;
		}

		private async Task demo()
		{
			HttpClient_Helper client = new HttpClient_Helper();
			string json = await client.GetAsyncDataByRapidAsync(
				"https://tiktok-all-in-one.p.rapidapi.com/user/videos?id=6603307560873934849&max_cursor=0",
				GetInstanceRoundRobin().Next().Token,
				"tiktok-all-in-one.p.rapidapi.com");
		}

		private async Task Run(byte thread)
		{
			await Task.Delay(1);
			return;
		}
	}
}
