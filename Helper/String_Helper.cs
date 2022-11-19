using Newtonsoft.Json;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SocialNetwork_New.Helper
{
	class String_Helper
	{
		public static readonly JsonSerializerOptions opt = new JsonSerializerOptions()
		{
			Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
			WriteIndented = true
		};

		public static string RemoveSpecialCharacter(string text)
		{
			if (String.IsNullOrEmpty(text))
			{
				return "";
			}

			return Regex.Replace(text, @"\t|\n|\r|\s+|&nbsp;|&amp;", " ");
		}
		public static T ToObject<T>(string json)
		{
			try
			{
				if (String.IsNullOrEmpty(json))
				{
					return default;
				}

				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (Exception)
			{
				return default;
			}
		}

		public static string ToJson<T>(T obj)
		{
			try
			{
				return System.Text.Json.JsonSerializer.Serialize<T>(obj, opt);
			}
			catch (Exception)
			{
				return default;
			}
		}

	}
}
