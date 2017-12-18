using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Security;

namespace Elmah.Net
{
    public static class CommonSettings
    {
		public static string SessionDomain { get { return ConfigurationManager.AppSettings.Get("Site:SessionDomain"); } }

		public static bool EnableSsl { get { return ConfigurationManager.AppSettings.Get("Site:EnableSsl").ParseBool(false); } }

		public static int SiteId { get { return ConfigurationManager.AppSettings.Get("Site:Id").ParseInt(0); } }

		public static string Application { get { return ConfigurationManager.AppSettings.Get("Site:Application"); } }

		public static string DatabaseName { get { return ConfigurationManager.AppSettings.Get("Site.DatabaseName"); } }

		public static string DeploymentPath { get { return ConfigurationManager.AppSettings.Get("Site.DeploymentPath"); } }

		// 0 = Clear / 1 = Encrypted / 2 = Hashed 
		public static MembershipPasswordFormat PasswordFormat { get { return ConfigurationManager.AppSettings.Get("Site:PasswordFormat").ParseEnum<MembershipPasswordFormat>(MembershipPasswordFormat.Hashed); } }

		public static string LoginPath { get { return ConfigurationManager.AppSettings.Get("Site.LoginPath"); } }


        public static string UrlPrefix { get { return ConfigurationManager.AppSettings.Get("Site:UrlPrefix"); } }
        public static string UrlPort { get { return ConfigurationManager.AppSettings.Get("Site:UrlPort"); } }

    }
}