using System;

namespace SocialNetwork_New.Helper
{
	class Date_Helper
	{
		public static double ConvertDateTimeToTimeStamp(DateTime value)
		{
			return (value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;
		}
	}
}
