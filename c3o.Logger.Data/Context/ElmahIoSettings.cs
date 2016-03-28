using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace c3o.Logger.Data
{
	public static class ElmahIoSettings
	{
		public static bool Log { get { return ConfigurationManager.AppSettings.Get("elmah.io:log") == "true"; } }
		public static string Name { get { return ConfigurationManager.AppSettings.Get("elmah.io:name"); } }
		public static string Key { get { return ConfigurationManager.AppSettings.Get("elmah.io:key"); } }
		public static string Url { get { return ConfigurationManager.AppSettings.Get("elmah.io:url"); } }
	}
}