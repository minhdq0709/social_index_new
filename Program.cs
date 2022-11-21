using SocialNetwork_New.Controller;
using System;
using System.Threading.Tasks;

namespace SocialNetwork_New
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("1: Get comment si_demand_source_post");
			Console.WriteLine("2: Get Tweet");
			Console.WriteLine("2: Tiktok si demand source");

			int choose = int.Parse(Console.ReadLine());
			switch (choose)
			{
				case 1:
					Facebook_Comment fb = new Facebook_Comment();

					while (true)
					{
						await fb.Crawl();
						await ContinueWith(TimeSpan.FromMinutes(5));
					}

				case 2:
					Twitter tw = new Twitter();
					while(true)
					{
						int totalTweet = await tw.CrawlData();
						Console.WriteLine("Total tweet: ", totalTweet);

						await ContinueWith(TimeSpan.FromHours(6));
					}

				case 3:
					Tiktok_Si_Demand_Source tksmd = new Tiktok_Si_Demand_Source();
					while (true)
					{
						await tksmd.Crawl(1);
						await ContinueWith(TimeSpan.FromHours(6));
					}
				default:
					break;
			}
		}

		private static async Task ContinueWith(TimeSpan delay)
		{
			Console.WriteLine($"Done at {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
			await Task.Delay(delay);
		}
	}
}
