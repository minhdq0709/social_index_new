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

			int choose = int.Parse(Console.ReadLine());
			switch (choose)
			{
				case 1:
					Facebook_Comment fb = new Facebook_Comment();

					while (true)
					{
						await fb.Crawl();

						Console.WriteLine($"Done at {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
						await Task.Delay(TimeSpan.FromMinutes(5));
					}

				default:
					break;
			}
		}
	}
}
