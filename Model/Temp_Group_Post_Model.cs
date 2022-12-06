using System;

namespace SocialNetwork_New.Model
{
	class Temp_Group_Post_Model
	{
		public int Id { set; get; }
		public DateTime Createdtime { set; get; }
		public DateTime Updatedtime { set; get; }
		public string Type { set; get; }
		public int Share { set; get; }
		public int Comment { set; get; }
		public int Reaction { set; get; }
		public double IdGroup { set; get; }
		public string TypeFB { set; get; }
		public byte IsReaction { set; get; }
		public byte IsComment { set; get; }
		public string IdPost { set; get; }
		public byte Status { set; get; }
		public DateTime Time { set; get; }
		public int CommentDownload { set; get; }
		public int ReactionDownload { set; get; }
		public byte Level { set; get; }
		public DateTime Getdate { set; get; }
		public DateTime Getupdate { set; get; }
		public DateTime Maxtimecomment { set; get; }

		public int Total_Comment { set; get; }
	}
}
