//using DbUp;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;

//namespace c3o.Logger.Data
//{
//	public class SiteRecord
//	{
//		public string Subdomain { get; set; }
//		public string Database { get; set; }
//		public string ConnectionString { get; set; }
//		public string DatabaseName { get { return string.Format(SiteSettings.DatabaseName, this.Database); } }

//		public SiteRecord(string subdomain, string database)
//		{
//			this.Subdomain = subdomain;
//			this.Database = database;

//			// Init Database
//			var connection = System.Configuration.ConfigurationManager.ConnectionStrings["LoggerContext"];

//			// connection string 
//			if (connection != null)
//			{
//				this.ConnectionString = string.Format(connection.ConnectionString, this.Database);
//			}

//		}


//		public bool CheckDb()
//		{
//			try
//			{
//				using (var connection = new SqlConnection(this.ConnectionString))
//				{
//					string sql = "select getdate()";

//					using (var command = new SqlCommand(sql, connection))
//					{
//						connection.Open();
//						return command.ExecuteScalar() != DBNull.Value;
//					}

//				}
//			}
//			catch
//			{
//				return false;
//			}
//		}

//		// check site db and create if needed
//		public bool CheckSiteDb()
//		{
//			var siteName = this.Subdomain;

//			if (!string.IsNullOrWhiteSpace(siteName))
//			{
//				if (this.CheckDb())
//				{
//					return this.UpdateDb();
//				}
//				else
//				{
//					// create db
//					if (this.CreateDb())
//					{
//						while (!this.CheckDb())
//						{
//							Thread.Sleep(1000);
//						}

//						// see if site is ok and check for update
//						return this.UpdateDb();
//					}
//				}
//			}
//			return false;
//		}




//		public bool CreateDb()
//		{
//			// get connection string
//			var conn = System.Configuration.ConfigurationManager.ConnectionStrings["SiteContext"];

//			if (conn != null)
//			{
//				// create db
//				try
//				{
//					using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
//					{
//						using (SqlCommand command = new SqlCommand())
//						{
//							connection.Open();
//							command.CommandText = "spCreateDatabase";
//							command.CommandType = CommandType.StoredProcedure;
//							command.Connection = connection;
//							command.Parameters.Add(new SqlParameter("@name", this.DatabaseName));
//							command.Parameters.Add(new SqlParameter("@path", SiteSettings.DatabasePath));
//							command.ExecuteNonQuery();
//							connection.Close();
//							return true;
//						}
//					}
//				}
//				catch
//				{
//					//Exception ex
//					return false;
//				}
//			}
//			return false;
//		}

//		public bool UpdateDb()
//		{
//			if (!string.IsNullOrWhiteSpace(SiteSettings.DeploymentPath))
//			{
//				// path to scripts
//				var path = HttpContext.Current.Server.MapPath(SiteSettings.DeploymentPath);

//				if (Directory.Exists(path))
//				{
//					try
//					{
//						// configure 
//						var upgrader = DeployChanges.To
//								.SqlDatabase(this.ConnectionString)
//								.WithScriptsFromFileSystem(path)
//								//.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
//								//.LogToConsole()
//								.Build();

//						if (upgrader.IsUpgradeRequired())
//						{
//							var response = upgrader.PerformUpgrade();
//							if (response.Successful)
//							{
//								return true;
//							}
//							else
//							{
//								//throw response.Error;
//								return false;
//							}
//						}
//						else
//						{
//							return true;
//						}
//					}
//					catch
//					{
//						return false;
//					}
//				}
//			}
//			return false;
//		}
//	}
//}
