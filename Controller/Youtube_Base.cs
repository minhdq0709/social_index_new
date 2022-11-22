using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_New.Controller
{
	class Youtube_Base
	{
		public Youtube_Detail_Video_Model SetDetailVideo(Youtube_Model.Item video)
		{
			return new Youtube_Detail_Video_Model
			{
				VideoCode = (string)video.id,
				ChannelId = 0,
				ChannelCode = video.snippet.channelId,
				CreateTime = video.snippet.publishedAt,
				Duration = null,

				Url = @"https://www.youtube.com/watch?v=" + (string)video.id,
				Title = video.snippet.title,
				Description = video.snippet.description,
				Thumbnails = video.snippet.thumbnails?.@default?.url,
				Author = video.snippet.channelTitle,

				LikeCount = video?.statistics?.likeCount ?? "0",
				ViewCount = video?.statistics?.viewCount ?? "0",
				FavoriteCount = video?.statistics?.favoriteCount ?? "0",
				CommentCount = video?.statistics?.commentCount ?? "0"
			};
		}
	}
}
