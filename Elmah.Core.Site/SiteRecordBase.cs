using Elmah.Net;
using DbUp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Elmah.Net.Logger.Data
{
	public class SiteRecordBase : ISiteRecord
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ConnectionString { get; set; }
				
		public SiteRecordBase()
		{			
			var connection = System.Configuration.ConfigurationManager.ConnectionStrings[CommonSettings.DatabaseName];

			// connection string 
			if (connection != null)
			{
				this.ConnectionString = connection.ConnectionString;
			}
		}
		

		// simple db test
		public bool CheckDb()
		{
			try
			{
				using (var connection = new SqlConnection(this.ConnectionString))
				{
					string sql = "select getdate()";

					using (var command = new SqlCommand(sql, connection))
					{
						connection.Open();
						return command.ExecuteScalar() != DBNull.Value;
					}

				}
			}
			catch
			{
				return false;
			}
		}


		// update db
		public bool UpdateDb()
		{
			if (!string.IsNullOrWhiteSpace(CommonSettings.DeploymentPath))
			{
				// path to scripts
				var path = HttpContext.Current.Server.MapPath(CommonSettings.DeploymentPath);

				if (Directory.Exists(path))
				{
					try
					{
						// configure 
						var upgrader = DeployChanges.To
								.SqlDatabase(this.ConnectionString)
								.WithScriptsFromFileSystem(path)
								//.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
								//.LogToConsole()
								.Build();

						if (upgrader.IsUpgradeRequired())
						{
							var response = upgrader.PerformUpgrade();
							if (response.Successful)
							{
								return true;
							}
							else
							{
								//throw response.Error;
								return false;
							}
						}
						else
						{
							return true;
						}
					}
					catch
					{
						return false;
					}
				}
			}
			return false;
		}
	}
}
