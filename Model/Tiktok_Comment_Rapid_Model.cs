using System.Collections.Generic;

namespace SocialNetwork_New.Model
{
	public class LabelList
	{
		public string text { get; set; }
		public int type { get; set; }
	}

	public class Avatar168x168
	{
		public int height { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
	}

	public class Avatar300x300
	{
		public int height { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
	}

	public class User1
	{
		public bool accept_private_policy { get; set; }
		public string account_region { get; set; }
		public object ad_cover_url { get; set; }
		public object advance_feature_item_order { get; set; }
		public int apple_account { get; set; }
		public int authority_status { get; set; }
		public Avatar168x168 avatar_168x168 { get; set; }
		public Avatar300x300 avatar_300x300 { get; set; }
		public AvatarLarger avatar_larger { get; set; }
		public AvatarMedium avatar_medium { get; set; }
		public AvatarThumb avatar_thumb { get; set; }
		public string avatar_uri { get; set; }
		public int aweme_count { get; set; }
		public string bind_phone { get; set; }
		public object bold_fields { get; set; }
		public object can_set_geofencing { get; set; }
		public object cha_list { get; set; }
		public int comment_filter_status { get; set; }
		public int comment_setting { get; set; }
		public int commerce_user_level { get; set; }
		public List<CoverUrl> cover_url { get; set; }
		public int create_time { get; set; }
		public string custom_verify { get; set; }
		public string cv_level { get; set; }
		public int download_prompt_ts { get; set; }
		public int download_setting { get; set; }
		public int duet_setting { get; set; }
		public string enterprise_verify_reason { get; set; }
		public object events { get; set; }
		public int favoriting_count { get; set; }
		public int fb_expire_time { get; set; }
		public int follow_status { get; set; }
		public int follower_count { get; set; }
		public int follower_status { get; set; }
		public object followers_detail { get; set; }
		public int following_count { get; set; }
		public object geofencing { get; set; }
		public string google_account { get; set; }
		public bool has_email { get; set; }
		public bool has_facebook_token { get; set; }
		public bool has_insights { get; set; }
		public bool has_orders { get; set; }
		public bool has_twitter_token { get; set; }
		public bool has_youtube_token { get; set; }
		public bool hide_search { get; set; }
		public object homepage_bottom_toast { get; set; }
		public string ins_id { get; set; }
		public bool is_ad_fake { get; set; }
		public bool is_block { get; set; }
		public bool is_discipline_member { get; set; }
		public bool is_phone_binded { get; set; }
		public bool is_star { get; set; }
		public object item_list { get; set; }
		public string language { get; set; }
		public int live_agreement { get; set; }
		public bool live_commerce { get; set; }
		public int live_verify { get; set; }
		public object mutual_relation_avatars { get; set; }
		public object need_points { get; set; }
		public int need_recommend { get; set; }
		public string nickname { get; set; }
		public object platform_sync_info { get; set; }
		public bool prevent_download { get; set; }
		public int react_setting { get; set; }
		public string region { get; set; }
		public object relative_users { get; set; }
		public int room_id { get; set; }
		public object search_highlight { get; set; }
		public string sec_uid { get; set; }
		public int secret { get; set; }
		public int shield_comment_notice { get; set; }
		public int shield_digg_notice { get; set; }
		public int shield_follow_notice { get; set; }
		public string short_id { get; set; }
		public bool show_image_bubble { get; set; }
		public string signature { get; set; }
		public int special_lock { get; set; }
		public int status { get; set; }
		public int stitch_setting { get; set; }
		public int total_favorited { get; set; }
		public int tw_expire_time { get; set; }
		public string twitter_id { get; set; }
		public string twitter_name { get; set; }
		public object type_label { get; set; }
		public string uid { get; set; }
		public string unique_id { get; set; }
		public int unique_id_modify_time { get; set; }
		public bool user_canceled { get; set; }
		public int user_mode { get; set; }
		public int user_period { get; set; }
		public int user_rate { get; set; }
		public object user_tags { get; set; }
		public int verification_type { get; set; }
		public string verify_info { get; set; }
		public VideoIcon video_icon { get; set; }
		public object white_cover_url { get; set; }
		public bool with_commerce_entry { get; set; }
		public bool with_shop_entry { get; set; }
		public string youtube_channel_id { get; set; }
		public string youtube_channel_title { get; set; }
		public int youtube_expire_time { get; set; }
		public string room_data { get; set; }
	}

	public class ReplyComment
	{
		public string aweme_id { get; set; }
		public string cid { get; set; }
		public int collect_stat { get; set; }
		public int create_time { get; set; }
		public int digg_count { get; set; }
		public bool is_author_digged { get; set; }
		public object label_list { get; set; }
		public string label_text { get; set; }
		public int label_type { get; set; }
		public bool no_show { get; set; }
		public object reply_comment { get; set; }
		public string reply_id { get; set; }
		public string reply_to_reply_id { get; set; }
		public int status { get; set; }
		public string text { get; set; }
		public List<object> text_extra { get; set; }
		public int trans_btn_style { get; set; }
		public User1 user { get; set; }
		public bool user_buried { get; set; }
		public int user_digged { get; set; }
	}

	public class Comment
	{
		public bool author_pin { get; set; }
		public string aweme_id { get; set; }
		public string cid { get; set; }
		public int collect_stat { get; set; }
		public int create_time { get; set; }
		public int digg_count { get; set; }
		public bool is_author_digged { get; set; }
		public List<LabelList> label_list { get; set; }
		public bool no_show { get; set; }
		public List<ReplyComment> reply_comment { get; set; }
		public int reply_comment_total { get; set; }
		public string reply_id { get; set; }
		public string reply_to_reply_id { get; set; }
		public int status { get; set; }
		public int stick_position { get; set; }
		public string text { get; set; }
		public List<object> text_extra { get; set; }
		public int trans_btn_style { get; set; }
		public User1 user { get; set; }
		public bool user_buried { get; set; }
		public int user_digged { get; set; }
	}

	public class Extra1
	{
		public object fatal_item_ids { get; set; }
		public long now { get; set; }
	}

	public class LogPb1
	{
		public string impr_id { get; set; }
	}

	class Tiktok_Comment_Rapid_Model
	{
		public bool alias_comment_deleted { get; set; }
		public List<Comment> comments { get; set; }
		public int cursor { get; set; }
		public Extra1 extra { get; set; }
		public int has_more { get; set; }
		public LogPb1 log_pb { get; set; }
		public int reply_style { get; set; }
		public int status_code { get; set; }
		public List<object> top_gifts { get; set; }
		public int total { get; set; }
	}
}
