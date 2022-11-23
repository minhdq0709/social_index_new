using SocialNetwork_New.Controller;
using System;
using System.Threading.Tasks;

namespace SocialNetwork_New
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("1: Get fb comment si_demand_source_post");
			Console.WriteLine("2: Get Tweet");
			Console.WriteLine("3: Tiktok si demand source");
			Console.WriteLine("4: Tiktok si demand source post");

			byte choose = byte.Parse(Console.ReadLine());
			byte totalThread = 4;

			switch (choose)
			{
				case 1:
					#region Case 1
					Facebook_Si_Demand_Source_Post fb = new Facebook_Si_Demand_Source_Post();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						try
						{
							await fb.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(5));
					}
				#endregion

				case 2:
					#region Case 2
					Twitter tw = new Twitter();
					while (true)
					{
						try
						{
							int totalTweet = await tw.CrawlData();
							Console.WriteLine("Total tweet: ", totalTweet);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromHours(6));
					}
				#endregion

				case 3:
					#region Case 3
					Tiktok_Si_Demand_Source tksmds = new Tiktok_Si_Demand_Source();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						try
						{
							await tksmds.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromHours(6));
					}
				#endregion

				case 4:
					#region Case 4
					Tiktok_Si_Demand_Source_Post tksmdsp = new Tiktok_Si_Demand_Source_Post();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						try
						{
							await tksmdsp.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(1));
					}
				#endregion

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
