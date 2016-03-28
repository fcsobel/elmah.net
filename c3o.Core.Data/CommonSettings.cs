using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Configuration;

namespace c3o.Web.Site.Data
{
	public static class CommonSettings
	{
		public static int SiteId { get { return Convert.ToInt32(ConfigurationManager.AppSettings.Get("site_id")); } }
		public static string Url { get { return ConfigurationManager.AppSettings.Get("site:url"); } }
		public static string ProductionUrl { get { return ConfigurationManager.AppSettings.Get("site:ProductionUrl"); } }

		public static string EmailServer { get { return ConfigurationManager.AppSettings.Get("email:server"); } }
		public static string EmailFrom { get { return ConfigurationManager.AppSettings.Get("email:from"); } }
		public static string EmailFromName { get { return ConfigurationManager.AppSettings.Get("email:fromname"); } }
		public static string EmailTo { get { return ConfigurationManager.AppSettings.Get("email:to"); } }
		public static string EmailBcc { get { return ConfigurationManager.AppSettings.Get("email:bcc"); } }
		public static string EmailSubject { get { return ConfigurationManager.AppSettings.Get("email:subject"); } }
		public static string EmailHelpdesk { get { return ConfigurationManager.AppSettings.Get("email:helpdesk"); } }
		
		public static bool EmailQueue { get { return ConfigurationManager.AppSettings.Get("email:queue") == "true"; } }

		public static string TempPath { get { return ConfigurationManager.AppSettings.Get("TempPath"); } }
		public static string TemplatePath { get { return ConfigurationManager.AppSettings.Get("Site:TemplatePath"); } }
		public static string LogPath { get { return ConfigurationManager.AppSettings.Get("Site:LogPath"); } }

		public static bool CheckDb { get { return ConfigurationManager.AppSettings.Get("CheckDb") == "true"; } }

		public static string Location { get { return ConfigurationManager.AppSettings.Get("Location"); } }
		public static string Site { get { return ConfigurationManager.AppSettings.Get("Site_Name"); } }
		public static string Environment { get { return ConfigurationManager.AppSettings.Get("Environment"); } }

        public static string SiteDefaultRoles { get { return ConfigurationManager.AppSettings.Get("site:DefaultRoles"); } }
		public static string SiteDistributorRoles { get { return ConfigurationManager.AppSettings.Get("site:DistributorRoles"); } }
		public static string SiteContractorRoles { get { return ConfigurationManager.AppSettings.Get("site:ContractorRoles"); } }

		

	}
}
