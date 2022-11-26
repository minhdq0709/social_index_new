using SocialNetwork_New.Controller;
using SocialNetwork_New.Schedule;
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
			Console.WriteLine("5: Youtube si demand source");
			Console.WriteLine("6: Youtube si demand source post");
			Console.WriteLine("7: Alert account fb died");
			Console.WriteLine("8: Update time count daily");

			byte choose = byte.Parse(Console.ReadLine());
			byte totalThread = 4;
			Scheduler sc = new Scheduler();

			switch (choose)
			{
				case 1:
					#region Case 1
					Facebook_Si_Demand_Source_Post fb = new Facebook_Si_Demand_Source_Post();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
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
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						try
						{
							int totalTweet = await tw.CrawlData();
							Console.WriteLine($"Total tweet: {totalTweet}");
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
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						try
						{
							await tksmds.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(5));
					}
				#endregion

				case 4:
					#region Case 4
					Tiktok_Si_Demand_Source_Post tksmdsp = new Tiktok_Si_Demand_Source_Post();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						try
						{
							await tksmdsp.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(5));
					}
				#endregion

				case 5:
					#region Case 5
					Youtube_Si_Demand_Source ytsdms = new Youtube_Si_Demand_Source();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						try
						{
							await ytsdms.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(5));
					}
				#endregion

				case 6:
					#region Case 6
					Youtube_Si_Demand_Source_Post ytsdmsp = new Youtube_Si_Demand_Source_Post();

					Console.WriteLine("Nhap so thread: ");
					totalThread = byte.Parse(Console.ReadLine());

					while (true)
					{
						Console.WriteLine($"Start at: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						try
						{
							await ytsdmsp.Crawl(totalThread);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

						await ContinueWith(TimeSpan.FromMinutes(30));
					}
				#endregion

				case 7:
					#region Case 7
					if (!await sc.CheckScheduleStartAsync())
					{
						await sc.StartAsync(Helper.Enum_Env_Helper.SCHEDULE.ALERT_TO_ACC_DIE, -1, 00);
					}
					else
					{
						await sc.StopAsync();
					}

					break;
				#endregion

				case 8:
					#region Case 8
					if (!await sc.CheckScheduleStartAsync())
					{
						await sc.StartAsync(Helper.Enum_Env_Helper.SCHEDULE.UPDATE_TIME_COUNT_TOKEN, 00, 01);
					}
					else
					{
						await sc.StopAsync();
					}

					break;
				#endregion

				default:
					Console.WriteLine("Chon ko hop le");
					break;
			}

			Console.ReadLine();
		}

		private static async Task ContinueWith(TimeSpan delay)
		{
			Console.WriteLine($"Done at {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
			await Task.Delay(delay);
		}
	}
}
