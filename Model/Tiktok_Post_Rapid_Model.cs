using System.Collections.Generic;

namespace SocialNetwork_New.Model
{
	public class Tiktok_Post_Rapid_Model
	{
		public AwemeDetail aweme_detail { get; set; }
		public Extra extra { get; set; }
		public LogPb log_pb { get; set; }
		public int status_code { get; set; }
	}

	public class AiDynamicCover
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class AiDynamicCoverBak
	{
		public List<string> url_list { get; set; }
		public string uri { get; set; }
	}

	public class AnimatedCover
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class Author
	{
		public object user_tags { get; set; }
		public int apple_account { get; set; }
		public int download_setting { get; set; }
		public bool with_commerce_entry { get; set; }
		public object relative_users { get; set; }
		public string share_qrcode_uri { get; set; }
		public int qa_status { get; set; }
		public bool is_phone_binded { get; set; }
		public string avatar_uri { get; set; }
		public AvatarLarger avatar_larger { get; set; }
		public string youtube_channel_title { get; set; }
		public object item_list { get; set; }
		public bool user_canceled { get; set; }
		public int follow_status { get; set; }
		public int total_favorited { get; set; }
		public int shield_digg_notice { get; set; }
		public bool has_orders { get; set; }
		public string signature { get; set; }
		public int download_prompt_ts { get; set; }
		public int aweme_count { get; set; }
		public object followers_detail { get; set; }
		public object advanced_feature_info { get; set; }
		public int need_recommend { get; set; }
		public bool live_commerce { get; set; }
		public bool is_discipline_member { get; set; }
		public object mutual_relation_avatars { get; set; }
		public int authority_status { get; set; }
		public int status { get; set; }
		public object geofencing { get; set; }
		public object can_set_geofencing { get; set; }
		public int mention_status { get; set; }
		public bool is_block { get; set; }
		public string account_region { get; set; }
		public int live_agreement { get; set; }
		public object search_highlight { get; set; }
		public string ins_id { get; set; }
		public int unique_id_modify_time { get; set; }
		public string youtube_channel_id { get; set; }
		public string sec_uid { get; set; }
		public ShareInfo share_info { get; set; }
		public int duet_setting { get; set; }
		public bool has_twitter_token { get; set; }
		public bool show_image_bubble { get; set; }
		public string language { get; set; }
		public int shield_follow_notice { get; set; }
		public bool prevent_download { get; set; }
		public bool has_email { get; set; }
		public int live_verify { get; set; }
		public int friends_status { get; set; }
		public int comment_setting { get; set; }
		public object advance_feature_item_order { get; set; }
		public int follower_count { get; set; }
		public bool has_facebook_token { get; set; }
		public string verify_info { get; set; }
		public object shield_edit_field_info { get; set; }
		public int fb_expire_time { get; set; }
		public int create_time { get; set; }
		public int special_lock { get; set; }
		public int youtube_expire_time { get; set; }
		public object white_cover_url { get; set; }
		public object platform_sync_info { get; set; }
		public int user_rate { get; set; }
		public string short_id { get; set; }
		public Avatar300x300 avatar_300x300 { get; set; }
		public object user_profile_guide { get; set; }
		public object type_label { get; set; }
		public int tw_expire_time { get; set; }
		public string twitter_id { get; set; }
		public string enterprise_verify_reason { get; set; }
		public object need_points { get; set; }
		public bool accept_private_policy { get; set; }
		public string cv_level { get; set; }
		public int secret { get; set; }
		public object homepage_bottom_toast { get; set; }
		public Avatar168x168 avatar_168x168 { get; set; }
		public int room_id { get; set; }
		public int user_period { get; set; }
		public AvatarThumb avatar_thumb { get; set; }
		public string bind_phone { get; set; }
		public int stitch_setting { get; set; }
		public bool hide_search { get; set; }
		public string twitter_name { get; set; }
		public string google_account { get; set; }
		public string uid { get; set; }
		public int user_mode { get; set; }
		public string custom_verify { get; set; }
		public bool with_shop_entry { get; set; }
		public object bold_fields { get; set; }
		public int verification_type { get; set; }
		public string nickname { get; set; }
		public bool has_youtube_token { get; set; }
		public object can_message_follow_status_list { get; set; }
		public string unique_id { get; set; }
		public object cha_list { get; set; }
		public AvatarMedium avatar_medium { get; set; }
		public bool is_star { get; set; }
		public VideoIcon video_icon { get; set; }
		public string region { get; set; }
		public List<CoverUrl> cover_url { get; set; }
		public int react_setting { get; set; }
		public int following_count { get; set; }
		public object ad_cover_url { get; set; }
		public int favoriting_count { get; set; }
		public object events { get; set; }
		public int commerce_user_level { get; set; }
		public int follower_status { get; set; }
		public bool has_insights { get; set; }
		public bool is_ad_fake { get; set; }
		public int shield_comment_notice { get; set; }
		public int comment_filter_status { get; set; }
	}

	public class AvatarLarger
	{
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
	}

	public class AvatarMedium
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class AvatarThumb
	{
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
	}

	public class AwemeAcl
	{
		public object platform_list { get; set; }
		public DownloadGeneral download_general { get; set; }
		public DownloadMaskPanel download_mask_panel { get; set; }
		public int share_list_status { get; set; }
		public ShareGeneral share_general { get; set; }
	}

	public class AwemeDetail
	{
		public object geofencing { get; set; }
		public object question_list { get; set; }
		public string sort_label { get; set; }
		public int collect_stat { get; set; }
		public string aweme_id { get; set; }
		public bool need_vs_entry { get; set; }
		public string anchors_extras { get; set; }
		public List<object> video_labels { get; set; }
		public string misc_info { get; set; }
		public Video video { get; set; }
		public List<object> content_desc_extra { get; set; }
		public ShareInfo share_info { get; set; }
		public object geofencing_regions { get; set; }
		public object nickname_position { get; set; }
		public bool without_watermark { get; set; }
		public VideoControl video_control { get; set; }
		public object long_video { get; set; }
		public List<object> mask_infos { get; set; }
		public string distance { get; set; }
		public int is_preview { get; set; }
		public long author_user_id { get; set; }
		public string share_url { get; set; }
		public string desc { get; set; }
		public object anchors { get; set; }
		public bool disable_search_trending_bar { get; set; }
		public RiskInfos risk_infos { get; set; }
		public LabelTop label_top { get; set; }
		public int item_stitch { get; set; }
		public object cover_labels { get; set; }
		public bool is_vr { get; set; }
		public AwemeAcl aweme_acl { get; set; }
		public bool cmt_swt { get; set; }
		public Author author { get; set; }
		public Statistics statistics { get; set; }
		public object commerce_config_data { get; set; }
		public object origin_comment_ids { get; set; }
		public int music_begin_time_in_ms { get; set; }
		public InteractPermission interact_permission { get; set; }
		public object search_highlight { get; set; }
		public object image_infos { get; set; }
		public int item_comment_settings { get; set; }
		public bool have_dashboard { get; set; }
		public int distribute_type { get; set; }
		public Status status { get; set; }
		public object position { get; set; }
		public int create_time { get; set; }
		public bool is_relieve { get; set; }
		public List<object> video_text { get; set; }
		public object green_screen_materials { get; set; }
		public bool prevent_download { get; set; }
		public int retry_type { get; set; }
		public int item_duet { get; set; }
		public int aweme_type { get; set; }
		public bool playlist_blocked { get; set; }
		public object hybrid_label { get; set; }
		public object products_info { get; set; }
		public bool has_vs_entry { get; set; }
		public int is_top { get; set; }
		public bool is_ads { get; set; }
		public object challenge_position { get; set; }
		public bool need_trim_step { get; set; }
		public int user_digged { get; set; }
		public object follow_up_publish_from_id { get; set; }
		public int bodydance_score { get; set; }
		public int item_react { get; set; }
		public object interaction_stickers { get; set; }
		public bool is_pgcshow { get; set; }
		public GroupIdList group_id_list { get; set; }
		public bool with_promotional_music { get; set; }
		public int is_hash_tag { get; set; }
		public string region { get; set; }
		public List<TextExtra> text_extra { get; set; }
		public int rate { get; set; }
		public int music_end_time_in_ms { get; set; }
		public object branded_content_accounts { get; set; }
		public List<ChaList> cha_list { get; set; }
		public string group_id { get; set; }
		public string desc_language { get; set; }
		public Music music { get; set; }
		public string content_desc { get; set; }
		public object label_top_text { get; set; }
		public CommerceInfo commerce_info { get; set; }
		public object uniqid_position { get; set; }
	}

	public class BitRate
	{
		public int bit_rate { get; set; }
		public PlayAddr play_addr { get; set; }
		public int is_bytevc1 { get; set; }
		public object dub_infos { get; set; }
		public string gear_name { get; set; }
		public int quality_type { get; set; }
	}

	public class ChaList
	{
		public bool is_pgcshow { get; set; }
		public bool is_commerce { get; set; }
		public int type { get; set; }
		public int is_challenge { get; set; }
		public ExtraAttr extra_attr { get; set; }
		public object search_highlight { get; set; }
		public object banner_list { get; set; }
		public string cid { get; set; }
		public int user_count { get; set; }
		public object cha_attrs { get; set; }
		public ShareInfo share_info { get; set; }
		public int sub_type { get; set; }
		public string cha_name { get; set; }
		public string hashtag_profile { get; set; }
		public string desc { get; set; }
		public List<object> connect_music { get; set; }
		public int collect_stat { get; set; }
		public int view_count { get; set; }
		public string schema { get; set; }
		public object show_items { get; set; }
		public Author author { get; set; }
	}

	public class ChorusInfo
	{
		public int duration_ms { get; set; }
		public int start_ms { get; set; }
	}

	public class CommerceInfo
	{
		public bool adv_promotable { get; set; }
		public bool auction_ad_invited { get; set; }
		public bool with_comment_filter_words { get; set; }
	}

	public class Cover
	{
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class CoverLarge
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class CoverMedium
	{
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class CoverThumb
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class CoverUrl
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class DownloadAddr
	{
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public int data_size { get; set; }
		public string uri { get; set; }
	}

	public class DownloadGeneral
	{
		public int show_type { get; set; }
		public string extra { get; set; }
		public int transcode { get; set; }
		public bool mute { get; set; }
		public int code { get; set; }
	}

	public class DownloadMaskPanel
	{
		public int transcode { get; set; }
		public bool mute { get; set; }
		public int code { get; set; }
		public int show_type { get; set; }
		public string extra { get; set; }
	}

	public class DurationHighPrecision
	{
		public object video_duration_precision { get; set; }
		public double duration_precision { get; set; }
		public double shoot_duration_precision { get; set; }
		public double audition_duration_precision { get; set; }
	}

	public class DynamicCover
	{
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
	}

	public class Extra
	{
		public long now { get; set; }
		public List<object> fatal_item_ids { get; set; }
		public string logid { get; set; }
	}

	public class ExtraAttr
	{
		public bool is_live { get; set; }
	}

	public class GroupIdList
	{
		public object GroupdIdList0 { get; set; }
		public List<long> GroupdIdList1 { get; set; }
	}

	public class InteractPermission
	{
		public int duet { get; set; }
		public int stitch { get; set; }
		public int duet_privacy_setting { get; set; }
		public int stitch_privacy_setting { get; set; }
		public int upvote { get; set; }
		public int allow_adding_to_story { get; set; }
	}

	public class LabelTop
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class LogPb
	{
		public string impr_id { get; set; }
	}

	public class MatchedSong
	{
		public string id { get; set; }
		public string author { get; set; }
		public string title { get; set; }
		public string h5_url { get; set; }
		public CoverMedium cover_medium { get; set; }
		public object performers { get; set; }
		public ChorusInfo chorus_info { get; set; }
	}

	public class Music
	{
		public bool is_pgc { get; set; }
		public object tag_list { get; set; }
		public int binded_challenge_id { get; set; }
		public int user_count { get; set; }
		public bool author_deleted { get; set; }
		public string album { get; set; }
		public object lyric_short_position { get; set; }
		public long id { get; set; }
		public int duration { get; set; }
		public object search_highlight { get; set; }
		public object position { get; set; }
		public string offline_desc { get; set; }
		public bool is_original { get; set; }
		public bool prevent_download { get; set; }
		public StrongBeatUrl strong_beat_url { get; set; }
		public string owner_nickname { get; set; }
		public string id_str { get; set; }
		public string title { get; set; }
		public int audition_duration { get; set; }
		public object multi_bit_rate_play_info { get; set; }
		public bool is_play_music { get; set; }
		public CoverLarge cover_large { get; set; }
		public int preview_end_time { get; set; }
		public int preview_start_time { get; set; }
		public int status { get; set; }
		public int video_duration { get; set; }
		public string extra { get; set; }
		public bool mute_share { get; set; }
		public bool can_not_reuse { get; set; }
		public bool is_author_artist { get; set; }
		public List<object> external_song_info { get; set; }
		public bool is_matched_metadata { get; set; }
		public CoverMedium cover_medium { get; set; }
		public int collect_stat { get; set; }
		public int shoot_duration { get; set; }
		public CoverThumb cover_thumb { get; set; }
		public string mid { get; set; }
		public MatchedSong matched_song { get; set; }
		public string owner_handle { get; set; }
		public string author { get; set; }
		public DurationHighPrecision duration_high_precision { get; set; }
		public object author_position { get; set; }
		public int source_platform { get; set; }
		public bool is_commerce_music { get; set; }
		public bool dmv_auto_show { get; set; }
		public bool is_audio_url_with_cookie { get; set; }
		public bool is_original_sound { get; set; }
		public PlayUrl play_url { get; set; }
		public List<object> artists { get; set; }
	}

	public class OriginCover
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class PlayAddr
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url_key { get; set; }
		public int data_size { get; set; }
		public string file_hash { get; set; }
		public string file_cs { get; set; }
	}

	public class PlayAddrBytevc1
	{
		public int width { get; set; }
		public int height { get; set; }
		public string url_key { get; set; }
		public int data_size { get; set; }
		public string file_hash { get; set; }
		public string file_cs { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class PlayAddrH264
	{
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url_key { get; set; }
		public int data_size { get; set; }
		public string file_hash { get; set; }
		public string file_cs { get; set; }
		public string uri { get; set; }
	}

	public class PlayUrl
	{
		public string uri { get; set; }
		public List<string> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class ReviewResult
	{
		public int review_status { get; set; }
	}

	public class RiskInfos
	{
		public bool warn { get; set; }
		public bool risk_sink { get; set; }
		public int type { get; set; }
		public string content { get; set; }
		public bool vote { get; set; }
	}

	public class ShareGeneral
	{
		public int code { get; set; }
		public int show_type { get; set; }
		public string extra { get; set; }
		public int transcode { get; set; }
		public bool mute { get; set; }
	}

	public class ShareInfo
	{
		public string share_link_desc { get; set; }
		public string share_signature_url { get; set; }
		public string share_title { get; set; }
		public string share_url { get; set; }
		public string share_title_myself { get; set; }
		public string share_quote { get; set; }
		public string share_desc_info { get; set; }
		public string share_signature_desc { get; set; }
		public string share_desc { get; set; }
		public int bool_persist { get; set; }
		public string share_title_other { get; set; }
		public ShareQrcodeUrl share_qrcode_url { get; set; }
	}

	public class ShareQrcodeUrl
	{
		public string uri { get; set; }
		public List<object> url_list { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Statistics
	{
		public int lose_count { get; set; }
		public string aweme_id { get; set; }
		public int comment_count { get; set; }
		public int forward_count { get; set; }
		public int play_count { get; set; }
		public int lose_comment_count { get; set; }
		public int share_count { get; set; }
		public int whatsapp_share_count { get; set; }
		public int collect_count { get; set; }
		public int digg_count { get; set; }
		public int download_count { get; set; }
	}
	public class Status
	{
		public ReviewResult review_result { get; set; }
		public bool allow_share { get; set; }
		public bool allow_comment { get; set; }
		public int reviewed { get; set; }
		public bool self_see { get; set; }
		public string aweme_id { get; set; }
		public int private_status { get; set; }
		public bool in_reviewing { get; set; }
		public bool is_delete { get; set; }
		public bool is_prohibited { get; set; }
		public int download_status { get; set; }
	}

	public class StrongBeatUrl
	{
		public int width { get; set; }
		public int height { get; set; }
		public string uri { get; set; }
		public List<string> url_list { get; set; }
	}

	public class TextExtra
	{
		public string sec_uid { get; set; }
		public int start { get; set; }
		public int end { get; set; }
		public string user_id { get; set; }
		public int type { get; set; }
		public string hashtag_name { get; set; }
		public string hashtag_id { get; set; }
		public bool is_commerce { get; set; }
	}

	public class Video
	{
		public object tags { get; set; }
		public string ratio { get; set; }
		public PlayAddrH264 play_addr_h264 { get; set; }
		public AnimatedCover animated_cover { get; set; }
		public bool has_watermark { get; set; }
		public PlayAddrBytevc1 play_addr_bytevc1 { get; set; }
		public int width { get; set; }
		public int duration { get; set; }
		public int is_bytevc1 { get; set; }
		public AiDynamicCoverBak ai_dynamic_cover_bak { get; set; }
		public string misc_download_addrs { get; set; }
		public int height { get; set; }
		public PlayAddr play_addr { get; set; }
		public AiDynamicCover ai_dynamic_cover { get; set; }
		public bool need_set_token { get; set; }
		public OriginCover origin_cover { get; set; }
		public List<BitRate> bit_rate { get; set; }
		public int is_long_video { get; set; }
		public string video_model { get; set; }
		public List<object> big_thumbs { get; set; }
		public Cover cover { get; set; }
		public DynamicCover dynamic_cover { get; set; }
		public string meta { get; set; }
		public DownloadAddr download_addr { get; set; }
		public int cdn_url_expired { get; set; }
		public bool is_callback { get; set; }
	}
	public class VideoControl
	{
		public bool allow_react { get; set; }
		public bool allow_download { get; set; }
		public int share_type { get; set; }
		public bool allow_music { get; set; }
		public int timer_status { get; set; }
		public bool allow_stitch { get; set; }
		public int show_progress_bar { get; set; }
		public int draft_progress_bar { get; set; }
		public bool allow_duet { get; set; }
		public int prevent_download_type { get; set; }
		public bool allow_dynamic_wallpaper { get; set; }
	}

	public class VideoIcon
	{
		public int height { get; set; }
		public string uri { get; set; }
		public List<object> url_list { get; set; }
		public int width { get; set; }
	}
}
