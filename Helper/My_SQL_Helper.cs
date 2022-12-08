using MySql.Data.MySqlClient;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialNetwork_New.Helper
{
	class My_SQL_Helper : IDisposable
	{
		private readonly MySqlConnection _conn;

		public My_SQL_Helper(string connection)
		{
			_conn = new MySqlConnection(connection);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (_conn != null && disposing)
			{
				if (_conn.State == System.Data.ConnectionState.Open)
				{
					_conn.Close();
				}

				_conn.Dispose();
			}
		}

		public List<SiDemandSource_Model> SelectFromTableSiDemandSource(int start, string platform)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source where platform = '{platform}' order by priority desc limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSource_Model> metaData = new List<SiDemandSource_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSource_Model
							{
								id = Convert.ToInt32(row["id"].ToString()),
								platform = row["platform"].ToString(),
								link = row["link"].ToString(),
								type = Convert.ToInt32(row["type"].ToString()),
								priority = Convert.ToInt32(row["priority"].ToString()),
								frequency = row["frequency"].ToString(),
								frequency_crawl_current_date = Convert.ToInt32(row["frequency_crawl_current_date"].ToString()),
								frequency_crawl_status_current_date = row["frequency_crawl_status_current_date"].ToString(),
								demand = row["demand"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString()),
								status = Convert.ToInt32(row["status"].ToString()),
								source_id = row["source_id"].ToString(),
								source_name = row["source_name"].ToString(),
								lock_tmp = Convert.ToInt32(row["lock_tmp"].ToString()),
								message_error = row["message_error"].ToString(),
								user_crawler = row["user_crawler"].ToString(),
								is_ended = Convert.ToInt32(row["is_ended"].ToString()),
								ticket_id = Convert.ToInt32(row["ticket_id"].ToString())
							});
						}
						catch (Exception ex) { }
					}
				}

				_conn.Close();
			}
			catch (Exception)
			{
				_conn.Close();
			}

			return metaData;
		}

		public List<Campain_Source_Links_Model> SelectFromTableCampaignSourceLink(int campainId)
		{
			_conn.Open();

			string query = $"SELECT * FROM FacebookDb.Campain_SourceLinks where CampainId = {campainId};";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<Campain_Source_Links_Model> metaData = new List<Campain_Source_Links_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new Campain_Source_Links_Model
							{
								Id = Convert.ToInt32(row["Id"].ToString()),
								CampainId = Convert.ToInt32(row["type"].ToString()),
								LinkId = Convert.ToInt32(row["priority"].ToString()),
								Url = row["frequency"].ToString(),
								Name = row["Name"].ToString()
							});
						}
						catch (Exception ex) { }
					}
				}

				_conn.Close();
			}
			catch (Exception)
			{
				_conn.Close();
			}

			return metaData;
		}

		public List<Temp_Group_Post_Model> SelectTemp_Group_Post(int start, DateTime dateCrawl, byte type)
		{
			_conn.Open();

			string query = $"SELECT * FROM FacebookDb.temp_grouppost where date(Getdate) = '{dateCrawl.ToString("yyyy-MM-dd")}'";

			if(type == 0)
			{
				query += " and Id % 2 = 0";
			}
			else
			{
				query += " and Id % 2 != 0";
			}

			query += $" order by Comment desc limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<Temp_Group_Post_Model> metaData = new List<Temp_Group_Post_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new Temp_Group_Post_Model
							{
								Id = Convert.ToInt32(row["Id"].ToString()),
								Createdtime = Convert.ToDateTime(row["Createdtime"].ToString()),
								IdPost = row["IdPost"].ToString()
							});
						}
						catch (Exception ex) { }
					}
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				_conn.Close();
			}

			return metaData;
		}

		public List<SiDemandSourcePost_Model> SelectFromTableSiDemandSourcePost(int start, string platform)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source_post where platform = '{platform}' and status in ({Config_System.NEW_DATA}, {Config_System.ERROR}) limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSourcePost_Model> metaData = new List<SiDemandSourcePost_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSourcePost_Model
							{
								id = Convert.ToInt32(row["id"].ToString()),
								post_id = row["post_id"].ToString(),
								link = row["link"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString()),
								title = row["title"].ToString(),
								content = row["content"].ToString(),
								user_crawler = row["user_crawler"].ToString()
							});
						}
						catch (Exception) { }
					}
				}

				_conn.Close();
			}
			catch (Exception)
			{
				_conn.Close();
			}

			return metaData;
		}

		public List<SiDemandSourcePost_Model> SelectFieldBaseFromTableSiDemandSourcePost(int start, string platform, byte type)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source_post where platform = '{platform}' and status in (0, -1)";
			if(type == 0)
			{
				query += " and id % 2 = 0"; 
			}
			else
			{
				query += " and id % 2 != 0";
			}

			query += $" limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSourcePost_Model> metaData = new List<SiDemandSourcePost_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSourcePost_Model
							{
								id = Convert.ToInt32(row["id"].ToString()),
								post_id = row["post_id"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString())
							});
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
					}
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				_conn.Close();
			}

			return metaData;
		}

		public List<Campaign_Post_Link_Model> SelectTableCampain_PostLinks(byte campaignId, byte type)
		{
			_conn.Open();

			string query = $"SELECT * FROM FacebookDb.Campain_PostLinks where CampainId = {campaignId} and CampainFlatFormTypeId in (2, 3) and CrawlerStatus = 0";

			if(type == 0)
			{
				query += " and Id % 2 = 0";
			}
			else
			{
				query += " and Id % 2 != 0";
			}

			query += " limit 0, 200;";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<Campaign_Post_Link_Model> metaData = new List<Campaign_Post_Link_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				while (row.Read())
				{
					metaData.Add(
						new Campaign_Post_Link_Model
						{
							Id = Convert.ToInt32(row["Id"]),
							CampainId = Convert.ToInt32(row["CampainId"]),
							SourceId = (string)row["SourceId"],
							PostId = (string)row["PostId"],
							CampainFlatFormTypeId = Convert.ToInt32(row["CampainFlatFormTypeId"]),
							LastUpdate = DateTime.Parse(row["LastUpdate"].ToString())
						});
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				if (_conn != null)
				{
					_conn.Close();
				}
			}

			return metaData;
		}

		public List<Si_Fb_Crawl_Comment_Model> SelectSi_Fb_Crawl_Comment(int start, sbyte status)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_fb_craw_comment where status = {status} limit {start}, 200;";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<Si_Fb_Crawl_Comment_Model> metaData = new List<Si_Fb_Crawl_Comment_Model>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new Si_Fb_Crawl_Comment_Model
							{
								id = Convert.ToInt32(row["id"].ToString()),
								post_id = row["post_id"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = string.IsNullOrEmpty(row["update_time"].ToString()) ? Convert.ToDateTime(row["create_time"].ToString()): Convert.ToDateTime(row["update_time"].ToString()),
								status = Convert.ToInt32(row["status"].ToString()),
								type = Convert.ToInt32(row["type"].ToString())
							});
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
					}
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				_conn.Close();
			}

			return metaData;
		}

		public List<AccessTokenFacebook> SelectToken(string statusToken)
		{
			_conn.Open();

			string query = $"SELECT Id, Token, StatusToken, FanPageName, Manager, User, FanPageLink, Timecount_token, Timecount_tokendie, Datetime_tokendie, Token_type, Cookies, Is_Page_Owner FROM FacebookDb.fb_tokens where StatusToken in ({statusToken});";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<AccessTokenFacebook> metaData = new List<AccessTokenFacebook>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							AccessTokenFacebook data = new AccessTokenFacebook();
							data.Id = Int32.Parse(row[0].ToString());
							data.Token = row[1].ToString();
							data.StatusToken = Int32.Parse(row[2].ToString());
							data.FanPageName = row[3].ToString();
							data.Manager = row[4].ToString();
							data.User = String_Helper.RemoveSpecialCharacter(row[5].ToString());
							data.FanPageLink = row[6].ToString();
							data.Timecount_token = Int32.Parse(row[7].ToString());
							data.Timecount_tokendie = Int32.Parse(row[8].ToString());
							data.Datetime_tokendie = string.IsNullOrEmpty(row[9].ToString()) ? DateTime.Now : DateTime.Parse(row[9].ToString());
							data.Token_type = Int32.Parse(row[10].ToString());
							data.Cookies = row[11].ToString();
							data.Is_Page_Owner = Boolean.Parse(row[12].ToString());

							metaData.Add(data);
						}
						catch (Exception ex)
						{
							File.AppendAllText($"{Environment.CurrentDirectory}/Check/SelectToken.txt", ex.ToString() + "\n" + query + "\n");
						}
					}
				}

				_conn.Close();
			}
			catch (Exception)
			{
				if (_conn != null)
				{
					_conn.Close();
				}
			}

			return metaData;
		}

		public List<AccessTokenFacebook> SelectTokenNotActiveByManager(string statusToken)
		{
			_conn.Open();

			string query = $"Select Manager, User, StatusToken, min(Datetime_tokendie) as Datetime_tokendie from FacebookDb.fb_tokens where StatusToken in ({statusToken}) group by Manager, User, StatusToken order by Manager;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<AccessTokenFacebook> metaData = new List<AccessTokenFacebook>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							AccessTokenFacebook data = new AccessTokenFacebook();
							data.StatusToken = Int32.Parse(row["StatusToken"].ToString());
							data.Manager = row["Manager"].ToString();
							data.User = String_Helper.RemoveSpecialCharacter(row["User"].ToString());
							data.Datetime_tokendie = string.IsNullOrEmpty(row["Datetime_tokendie"].ToString()) ? DateTime.Now : DateTime.Parse(row["Datetime_tokendie"].ToString());

							metaData.Add(data);
						}
						catch (Exception ex)
						{
							File.AppendAllText($"{Environment.CurrentDirectory}/Check/SelectTokenNotActiveByManager.txt", ex.ToString() + "\n" + query + "\n");
						}
					}
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/SelectTokenNotActiveByManager.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}

			return metaData;
		}

		public void UpdateNumberUseToken(AccessTokenFacebook data)
		{
			_conn.Open();

			string query = $"UPDATE FacebookDb.fb_tokens SET Timecount_token = {data.Timecount_token}, " +
				$"Timecount_tokendie = {data.Timecount_tokendie} " +
				$"where Id = {data.Id};";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateStatusTokenByUser.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public uint InsertToTableFb_Token_History(AccessTokenFacebook data)
		{
			try
			{
				_conn.Open();

				string query = "Insert into FacebookDb.fb_tokens_history(fb_tokens_id, access_token, postdate, status, manager, user, fan_page_link) " +
					"values(@fb_tokens_id, @access_token, @postdate, @status, @manager, @user, @fan_page_link);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				cmd.CommandText = query;

				cmd.Parameters.Add("@fb_tokens_id", MySqlDbType.Int32).Value = data.Id;
				cmd.Parameters.Add("@access_token", MySqlDbType.VarChar).Value = data.Token;
				cmd.Parameters.Add("@status", MySqlDbType.Int32).Value = data.StatusToken == 1 ? 0 : 1; // 1: die, 0: live
				cmd.Parameters.Add("@postdate", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@manager", MySqlDbType.VarChar).Value = data.Manager;
				cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = data.User;
				cmd.Parameters.Add("@fan_page_link", MySqlDbType.VarChar).Value = data.FanPageLink;

				uint row = (uint)cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				if (_conn != null)
				{
					_conn.Close();
				}

				File.AppendAllText($"{Environment.CurrentDirectory}/Check/InsertToTableFb_Token_History.txt", ex.ToString() + "\n");
				return 0;
			}
		}

		public int InsertToTableSi_demand_source_post(Tiktok_Post_Kafka_Model data)
		{
			try
			{
				_conn.Open();

				string query = "Insert into social_index_v2.si_demand_source_post(post_id, platform, link, create_time, update_time, status) " +
					"values(@post_id, @platform, @link, @create_time, @update_time, @status);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				cmd.CommandText = query;

				cmd.Parameters.Add("@post_id", MySqlDbType.VarChar).Value = data.IdVideo;
				cmd.Parameters.Add("@platform", MySqlDbType.VarChar).Value = "tiktok";
				cmd.Parameters.Add("@link", MySqlDbType.VarChar).Value = data.LinkVideo;
				cmd.Parameters.Add("@create_time", MySqlDbType.Timestamp).Value = data.TimeCreated;
				cmd.Parameters.Add("@update_time", MySqlDbType.Timestamp).Value = data.TimeCreated;
				cmd.Parameters.Add("@status", MySqlDbType.Int32).Value = Config_System.NEW_DATA;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				if (_conn != null)
				{
					_conn.Close();
				}

				File.AppendAllText($"{Environment.CurrentDirectory}/Check/InsertToTableFb_Token_History.txt", ex.ToString() + "\n");
				return 0;
			}
		}

		public int InsertToTableSi_demand_source_post(Youtube_Detail_Video_Kafka_Model data)
		{
			try
			{
				_conn.Open();

				string query = "Insert into social_index_v2.si_demand_source_post(post_id, platform, link, create_time, update_time, status) " +
					"values(@post_id, @platform, @link, @create_time, @update_time, @status);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				cmd.CommandText = query;

				cmd.Parameters.Add("@post_id", MySqlDbType.VarChar).Value = data.VideoCode;
				cmd.Parameters.Add("@platform", MySqlDbType.VarChar).Value = "youtube";
				cmd.Parameters.Add("@link", MySqlDbType.VarChar).Value = data.Url;
				cmd.Parameters.Add("@create_time", MySqlDbType.Timestamp).Value = data.CreateTime;
				cmd.Parameters.Add("@update_time", MySqlDbType.Timestamp).Value = data.CreateTime;
				cmd.Parameters.Add("@status", MySqlDbType.Int32).Value = Config_System.NEW_DATA;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				if (_conn != null)
				{
					_conn.Close();
				}

				File.AppendAllText($"{Environment.CurrentDirectory}/Check/InsertToTableFb_Token_History.txt", ex.ToString() + "\n");
				return 0;
			}
		}

		public int InsertToTableHistoryFbPost(SiDemandSourcePost_Model data)
		{
			try
			{
				_conn.Open();

				string query = "INSERT IGNORE INTO FacebookDb.History_Fb_Post(source_post_id, create_time_post, comments_real, shares_real) " +
						"VALUES(@source_post_id, @create_time_post, @comments_real, @shares_real)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				;
				cmd.CommandText = query;

				cmd.Parameters.Add("@source_post_id", MySqlDbType.VarChar).Value = data.post_id;
				cmd.Parameters.Add("@create_time_post", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@comments_real", MySqlDbType.Int32).Value = data.total_comment;
				cmd.Parameters.Add("@shares_real", MySqlDbType.Int32).Value = data.total_share;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/Json3.txt", ex.ToString() + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}

				return 0;
			}
		}

		public int InsertToTableCampain_Contents_Facebook_Post(Campain_Contents_Facebook_Post_Model data)
		{
			try
			{
				_conn.Open();

				string query = "INSERT IGNORE INTO FacebookDb.Campain_Contents_Facebook_Post(Id, PostID, Url, PostDate, LastDate, " +
					"CountComment, CountLike, CountShare, AuthorId, AuthorName, " +
					"PlatFromType, Content, Hastag, Reactions, Si_status) " +
						"VALUES(@Id, @PostID, @Url, @PostDate, @LastDate," +
						"@CountComment, @CountLike, @CountShare,@AuthorId, @AuthorName," +
						"@PlatFromType, @Content, @Hastag, @Reactions, @Si_status)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				;
				cmd.CommandText = query;

				cmd.Parameters.Add("@source_post_id", MySqlDbType.VarChar).Value = data.post_id;
				cmd.Parameters.Add("@create_time_post", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@comments_real", MySqlDbType.Int32).Value = data.total_comment;
				cmd.Parameters.Add("@shares_real", MySqlDbType.Int32).Value = data.total_share;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/Json3.txt", ex.ToString() + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}

				return 0;
			}
		}

		public int InsertToTableHistoryFbPost(Temp_Group_Post_Model data)
		{
			try
			{
				_conn.Open();

				string query = "INSERT IGNORE INTO FacebookDb.History_Fb_Post(source_post_id, create_time_post, comments_real) " +
						"VALUES(@source_post_id, @create_time_post, @comments_real)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				;
				cmd.CommandText = query;

				cmd.Parameters.Add("@source_post_id", MySqlDbType.VarChar).Value = data.IdPost;
				cmd.Parameters.Add("@create_time_post", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@comments_real", MySqlDbType.Int32).Value = data.Total_Comment;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/Json3.txt", ex.ToString() + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}

				return 0;
			}
		}

		public int InsertToTableHistoryFbPost(Campaign_Post_Link_Model data)
		{
			try
			{
				_conn.Open();

				string query = "INSERT IGNORE INTO FacebookDb.History_Fb_Post(source_post_id, create_time_post, comments_real) " +
						"VALUES(@source_post_id, @create_time_post, @comments_real)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				;
				cmd.CommandText = query;

				cmd.Parameters.Add("@source_post_id", MySqlDbType.VarChar).Value = data.PostId;
				cmd.Parameters.Add("@create_time_post", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@comments_real", MySqlDbType.Int32).Value = data.Comments;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/Json3.txt", ex.ToString() + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}

				return 0;
			}
		}

		public int InsertToTableHistoryFbPost(Si_Fb_Crawl_Comment_Model data)
		{
			try
			{
				_conn.Open();

				string query = "INSERT IGNORE INTO FacebookDb.History_Fb_Post(source_post_id, create_time_post, comments_real) " +
						"VALUES(@source_post_id, @create_time_post, @comments_real)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = _conn;
				;
				cmd.CommandText = query;

				cmd.Parameters.Add("@source_post_id", MySqlDbType.VarChar).Value = data.post_id;
				cmd.Parameters.Add("@create_time_post", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add("@comments_real", MySqlDbType.Int32).Value = data.total_comment;

				int row = cmd.ExecuteNonQuery();
				_conn.Close();

				return row;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/Json3.txt", ex.ToString() + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}

				return 0;
			}
		}

		public List<Token_Yt_Tiktok_TwitterModel> SelectTokenYT_Tiktok_Twitetr(byte type)
		{
			_conn.Open();

			string query = $"SELECT * FROM FacebookDb.Token_YT_Tiktok where Status_ = {type} order by Number_Of_Uesd_To_Token desc;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<Token_Yt_Tiktok_TwitterModel> metaData = new List<Token_Yt_Tiktok_TwitterModel>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							Token_Yt_Tiktok_TwitterModel data = new Token_Yt_Tiktok_TwitterModel();
							data.Id = Int32.Parse(row["Id"].ToString());
							data.Token = row["Token"].ToString();
							data.Status_ = Int32.Parse(row["Status_"].ToString());
							data.Number_Of_Uesd_To_Token = Int32.Parse(row["Number_Of_Uesd_To_Token"].ToString());

							metaData.Add(data);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
					}
				}

				_conn.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				if (_conn != null)
				{
					_conn.Close();
				}
			}

			return metaData;
		}

		public void UpdateTimeAndStatusAndUsernameToTableSiDemandSourcePost(SiDemandSourcePost_Model data)
		{
			_conn.Open();

			string query = $"UPDATE social_index_v2.si_demand_source_post SET update_time = @update_time, status = @status, user_crawler = @user_crawler where Id = @Id;";

			MySqlCommand cmd = new MySqlCommand();

			cmd.Connection = _conn;
			cmd.CommandText = query;

			cmd.Parameters.AddWithValue("@update_time", DateTime.Now);
			cmd.Parameters.AddWithValue("@status", data.status);
			cmd.Parameters.AddWithValue("@user_crawler", "Minhdq");
			cmd.Parameters.AddWithValue("@Id", data.id);

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateStatusTokenByUser.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public void UpdateTimeAndStatusAndFrequencyCrawlStatutToTableSiDemandSource(SiDemandSource_Model data)
		{
			_conn.Open();

			string query = $"UPDATE social_index_v2.si_demand_source SET update_time = @update_time, " +
				$"status = @status, " +
				$"frequency_crawl_current_date = @frequency_crawl_current_date, " +
				$"frequency_crawl_status_current_date = @frequency_crawl_status_current_date," +
				$"update_time_crawl = @update_time_crawl," +
				$"user_crawler = @user_crawler where Id = @Id;";

			MySqlCommand cmd = new MySqlCommand();

			cmd.Connection = _conn;
			cmd.CommandText = query;

			cmd.Parameters.AddWithValue("@update_time", data.update_time);
			cmd.Parameters.AddWithValue("@status", data.status);
			cmd.Parameters.AddWithValue("@frequency_crawl_status_current_date", data.frequency_crawl_status_current_date);
			cmd.Parameters.AddWithValue("@frequency_crawl_current_date", data.frequency_crawl_current_date);
			cmd.Parameters.AddWithValue("@update_time_crawl", data.update_time_crawl);
			cmd.Parameters.AddWithValue("@user_crawler", data.user_crawler);
			cmd.Parameters.AddWithValue("@Id", data.id);

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateStatusTokenByUser.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public void UpdateTimeCountTokenDaily()
		{
			_conn.Open();

			string query = $"UPDATE FacebookDb.fb_tokens SET Timecount_token = 0, Timecount_tokendie = 0 where StatusToken in ({Config_System.USER_LIVE}, {Config_System.USER_DIE}, {Config_System.GET_TOKEN_BACK});";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateTimeCountTokenDaily.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public void UpdateStatusAndUpdateTime_Si_Fb_Crawl_Comment(Si_Fb_Crawl_Comment_Model data)
		{
			_conn.Open();

			string query = $"UPDATE social_index_v2.si_fb_craw_comment SET status = {data.status}, " +
				$"update_time = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
				$"where id = {data.id};";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateStatusAndUpdateTime_Si_Fb_Crawl_Comment.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public void UpdateCrawlerStatus_Campain_PostLinks(Campaign_Post_Link_Model data)
		{
			_conn.Open();

			string query = $"UPDATE FacebookDb.Campain_PostLinks SET CrawlerStatus = {data.CrawlerStatus} where id = {data.Id};";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateCrawlerStatus_Campain_PostLinks.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
				}
			}
		}

		public void UpdateStatusTokenByUser(IEnumerable<string> listUserName, int status)
		{
			_conn.Open();

			string query = $"UPDATE FacebookDb.fb_tokens SET StatusToken = {status}, Datetime_tokendie = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
				$"where User in('{listUserName.Aggregate((x, y) => x + "','" + y)}') and StatusToken = {Config_System.USER_LIVE}";

			if (status == Config_System.GET_TOKEN_BACK)
			{
				query += " and Is_Page_Owner = 1";
			}

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdateStatusTokenByUser.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
					_conn.Dispose();
				}
			}
		}

		public void UpdatePageInvalidate(IEnumerable<string> listId, string status)
		{
			_conn.Open();

			string query = $"UPDATE FacebookDb.fb_tokens SET StatusToken = {status}, Datetime_tokendie = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
				$"where id in({listId.Aggregate((x, y) => x + "," + y)})";

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			try
			{
				cmd.ExecuteNonQuery();
				_conn.Close();
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/UpdatePageInvalidate.txt", ex.ToString() + "\n" + query + "\n");
				if (_conn != null)
				{
					_conn.Close();
					_conn.Dispose();
				}
			}
		}
	}
}
