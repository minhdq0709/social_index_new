using System;

namespace SocialNetwork_New.Helper
{
	class Date_Helper
	{
		public static double ConvertDateTimeToTimeStamp(DateTime value)
		{
			return (value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;
		}

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}
