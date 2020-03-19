using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Elmah.Net
{
	public static class UriHelper
	{
		public static string GetLoginSite(this HttpRequestBase context)
		{
			var path = CommonSettings.LoginSite;

			// set domain
			if (path.Contains("{0}")) { path = string.Format(path, context.Url.LastNodes("elmah")); }

			return path;
		}

		public static string GetLoginSite(this HttpRequest context)
		{
			var path = CommonSettings.LoginSite;

			// set domain
			if (path.Contains("{0}")) { path = string.Format(path, context.Url.LastNodes("elmah")); }

			return path;
		}

		public static string GetLoginPath(this HttpRequestBase context)
		{
			return context.GetLoginSite() + CommonSettings.LoginPath;
		}
		public static string GetLoginPath(this HttpRequest context)
		{
			return context.GetLoginSite() + CommonSettings.LoginPath;
		}

		public static string LastNodes(this Uri uri, string node)
		{
			var list = uri.Host.Split('.').ToList();
			var index = list.IndexOf(node);
			if (index > 0)
			{
				return string.Join(".", list.Skip(index).ToList());
			}
			return uri.LastTwo();
		}

		public static string LastTwo(this Uri uri)
		{
			var list = uri.Host.Split('.');
			if (list.Length > 1)
			{
				return string.Format("{0}.{1}", list[list.Length - 2], list[list.Length - 1]);
			}
			else
			{
				return list[0];
			}
		}
	}
}
