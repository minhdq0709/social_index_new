using System.Collections.Generic;

namespace SocialNetwork_New.Model
{
	public class Media
	{
		public string source { get; set; }
	}

	public class From
	{
		public string name { get; set; }
		public string id { get; set; }
	}

	public class Datum2
	{
		public string created_time { get; set; }
		public string full_picture { get; set; }
		public string message { get; set; }
		public string permalink_url { get; set; }
		public string id { get; set; }
		public string post_id { set; get; }
		public Attachments attachments { get; set; }
		public From from { get; set; }
	}

	public class Datum
	{
		public Media media { get; set; }
	}

	public class Attachments
	{
		public List<Datum> data { get; set; }
	}

	public class Cursors
	{
		public string before { get; set; }
		public string after { get; set; }
	}

	public class Paging
	{
		public Cursors cursors { get; set; }
		public string next { get; set; }
	}

	public class Error
	{
		public string message { get; set; }
		public string type { get; set; }
		public int code { get; set; }
		public string fbtrace_id { get; set; }
	}

	public class FacebookModel
	{
		public List<Datum2> data { get; set; }
		public Paging paging { get; set; }
		public Error error { get; set; }
	}
}
