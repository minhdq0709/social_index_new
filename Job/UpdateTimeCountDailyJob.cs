using Quartz;
using SocialNetwork_New.Helper;
using System.Threading.Tasks;

namespace SocialNetwork_New.Job
{
	class UpdateTimeCountDailyJob : IJob
	{
		public Task Execute(IJobExecutionContext context)
		{
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				mysql.UpdateTimeCountTokenDaily();
			}

			return Task.CompletedTask;
		}
	}
}
