using Quartz;
using Quartz.Impl;
using SocialNetwork_New.Job;
using System.Threading.Tasks;

namespace SocialNetwork_New.Schedule
{
	class Scheduler
	{
		private static IScheduler scheduler;
		public async Task StartAsync(Helper.Enum_Env_Helper.SCHEDULE sc, int hour = 0, int minute = 0)
		{
			await scheduler.Start();

			switch (sc)
			{
				case Helper.Enum_Env_Helper.SCHEDULE.ALERT_TO_ACC_DIE:
					{
						IJobDetail jobAlertAccDie = JobBuilder.Create<Alert_Account_Fb_Die_Job>()
							.WithIdentity("jobAlertAccDie")
							.Build();

						ITrigger triggerAlertAccDie = CreateTrigger("triggerAlertAccDie", hour, minute);
						await scheduler.ScheduleJob(jobAlertAccDie, triggerAlertAccDie);
						return;
					}

				case Helper.Enum_Env_Helper.SCHEDULE.UPDATE_TIME_COUNT_TOKEN:
					{
						IJobDetail jobUpdateTimeCountTokendaily = JobBuilder.Create<UpdateTimeCountDailyJob>()
							.WithIdentity("JobUpdateTimeCountTokendaily")
							.Build();

						ITrigger triggerUpdateTimeCountTokendaily = CreateTrigger("triggerUpdateTimeCountTokendaily", hour, minute);
						await scheduler.ScheduleJob(jobUpdateTimeCountTokendaily, triggerUpdateTimeCountTokendaily);
						return;
					}
			}
		}

		public async Task<bool> CheckScheduleStartAsync()
		{
			scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			return scheduler.IsStarted;
		}

		public async Task StopAsync()
		{
			if (scheduler.IsStarted)
			{
				await scheduler.Shutdown();
			}
		}

		public async Task ResetSchedule(string keyTrigger, int hour = 0, int minute = 0)
		{
			ITrigger newTrigger = CreateTrigger(keyTrigger, hour, minute);
			await scheduler.RescheduleJob(new TriggerKey(keyTrigger), newTrigger);
		}

		private ITrigger CreateTrigger(string key, int hour, int minute)
		{
			if (hour < 0)
			{
				return TriggerBuilder.Create()
				  .WithIdentity(key)
				  .StartNow()
				  .WithSimpleSchedule(x => x
					  .WithIntervalInHours(hour * (-1))
					  .RepeatForever())
				  .Build();
			}

			return TriggerBuilder.Create()
				.WithIdentity(key)
				.WithDailyTimeIntervalSchedule
					(s =>
						s.WithIntervalInHours(24)
					.OnEveryDay()
					.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hour, minute))
					)
				.Build();
		}
	}
}
