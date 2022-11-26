using Quartz;
using SocialNetwork_New.Helper;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_New.Job
{
	class Alert_Account_Fb_Die_Job : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			IEnumerable<AccessTokenFacebook> listToken = null;
			using (My_SQL_Helper mysql = new My_SQL_Helper(Config_System.DB_FB_51_79))
			{
				listToken = mysql.SelectTokenNotActiveByManager($"{Config_System.USER_DIE}, {Config_System.GET_TOKEN_BACK}");
			}

			if(listToken != null)
			{
				await SendMessageToGroup(listToken);
			}
		}

		private async Task SendMessageToGroup(IEnumerable<AccessTokenFacebook> data)
		{
			Telegram_Helper bot = new Telegram_Helper(Config_System.KEY_BOT_TIKTOK);
			StringBuilder sb = new StringBuilder();
			List<string> lstMess = new List<string>();
			string checkNewManager = "";

			foreach(AccessTokenFacebook item in data)
			{
				if(!checkNewManager.Equals(item.Manager))
				{
					sb.Append($"❗ <b>{item.Manager}</b>:\n");
					checkNewManager = item.Manager;
				}

				if (sb.Length > 3800)
				{
					lstMess.Add(sb.ToString());
					sb.Clear();
				}

				sb.Append($"- {item.User} | ");

				if (item.StatusToken == Config_System.GET_TOKEN_BACK)
				{
					sb.Append("Lấy lại token | ");
				}

				else if (item.StatusToken == Config_System.USER_DIE)
				{
					sb.Append("User die | ");
				}

				sb.Append(CalculatorTime(DateTime.Now, item.Datetime_tokendie));
			}

			lstMess.Add(sb.ToString());
			sb.Clear();

			foreach (string item in lstMess)
			{
				await bot.SendMessageToChannel(item, Config_System.ID_GROUP_CRAWL_DATA_CHECK_LIVE_ACC);
				await Task.Delay(5_000);
			}
		}

		private string CalculatorTime(DateTime time1, DateTime time2)
		{
			TimeSpan duration = time1 - time2;
			long durationTicks = Math.Abs(duration.Ticks / TimeSpan.TicksPerMillisecond);
			long hours = durationTicks / (1000 * 60 * 60);
			long minutes = (durationTicks - (hours * 60 * 60 * 1000)) / (1000 * 60);

			return $"<b>{hours.ToString("00")}h{minutes.ToString("00")}p</b>\n";
		}
	}
}
