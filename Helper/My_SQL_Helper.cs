using MySql.Data.MySqlClient;
using SocialNetwork_New.Model;
using System;
using System.Collections.Generic;
using System.IO;

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

		public List<SiDemandSource> SelectFromTableSiDemandSource(int start, int priority, string platform, int ticket_id)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source where platform = '{platform}' and priority = {priority} and ticket_id = {ticket_id} limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSource> metaData = new List<SiDemandSource>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSource
							{
								id = Convert.ToInt32(row["id"].ToString()),
								platform = row["platform"].ToString(),
								link = row["link"].ToString(),
								type = Convert.ToInt32(row["type"].ToString()),
								priority = Convert.ToInt32(row["priority"].ToString()),
								frequency = row["frequency"].ToString(),
								frequency_crawl_current_date = Convert.ToInt32(row["frequency_crawl_current_date"].ToString()),
								demand = row["demand"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString()),
								update_time_crawl = Convert.ToDateTime(row["update_time_crawl"].ToString()),
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

		public List<SiDemandSourcePost> SelectFromTableSiDemandSourcePost(int start, string platform)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source_post where platform = '{platform}' limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSourcePost> metaData = new List<SiDemandSourcePost>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSourcePost
							{
								id = Convert.ToInt32(row["id"].ToString()),
								si_demand_source_id = Convert.ToInt32(row["si_demand_source_id"].ToString()),
								post_id = row["post_id"].ToString(),
								link = row["link"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString()),
								status = Convert.ToInt32(row["status"].ToString()),
								title = row["title"].ToString(),
								content = row["content"].ToString(),
								total_comment = Convert.ToInt32(row["total_comment"].ToString()),
								total_like = Convert.ToInt32(row["total_like"].ToString()),
								total_share = Convert.ToInt32(row["total_share"].ToString()),
								user_crawler = row["user_crawler"].ToString(),
								lock_tmp = Convert.ToInt32(row["lock_tmp"].ToString())
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

		public List<SiDemandSourcePost> SelectFieldBaseFromTableSiDemandSourcePost(int start, string platform)
		{
			_conn.Open();

			string query = $"SELECT * FROM social_index_v2.si_demand_source_post where platform = '{platform}' limit {start}, 200;";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = _conn;
			cmd.CommandText = query;

			List<SiDemandSourcePost> metaData = new List<SiDemandSourcePost>();
			try
			{
				MySqlDataReader row = cmd.ExecuteReader();

				if (row.HasRows)
				{
					while (row.Read())
					{
						try
						{
							metaData.Add(new SiDemandSourcePost
							{
								id = Convert.ToInt32(row["id"].ToString()),
								post_id = row["post_id"].ToString(),
								create_time = Convert.ToDateTime(row["create_time"].ToString()),
								update_time = Convert.ToDateTime(row["update_time"].ToString())
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
						catch (Exception)
						{
							Console.WriteLine();
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
				cmd.Parameters.Add("@postdate", MySqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

				File.AppendAllText($"{Environment.CurrentDirectory}/Check/err.txt", ex.ToString() + "\n");
				return 0;
			}
		}

		public int InsertToTableHistoryFbPost(SiDemandSourcePost data)
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
	}
}
