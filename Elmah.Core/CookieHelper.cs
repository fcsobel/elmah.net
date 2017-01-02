using c3o.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace c3o.Core
{
	public static class CookieHelper
	{
		/// <summary>
		/// Get Cookie value
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetCookie(this HttpContext context, string name)
		{
			HttpCookie cookie = null;
			if (context.Response.Cookies.AllKeys.Contains(name))
			{
				cookie = context.Response.Cookies[name];
			}
			else
			{
				cookie = context.Request.Cookies[name];
			}
			return cookie != null ? HttpUtility.UrlDecode(cookie.Value) : null;
		}


		/// <summary>
		/// Set Cookie Value
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="expires"></param>
		/// <param name="httpOnly"></param>
		/// <param name="secure"></param>
		/// <param name="domain"></param>
		public static void SetCookie(this HttpContext context, string name, string value, DateTime? expires = null, bool httpOnly = true, bool secure = false, string domain = null)
		{
			if (String.IsNullOrEmpty(value))
			{
				context.ClearCookie(name);
			}
			else
			{
				//HttpCookie cookie = context.Response.Cookies.AllKeys.Contains(name) && !String.IsNullOrEmpty(context.Response.Cookies[name].Value) ? context.Response.Cookies[name] : HttpContext.Current.Request.Cookies.AllKeys.Contains(name) && !String.IsNullOrEmpty(HttpContext.Current.Request.Cookies[name].Value) ? HttpContext.Current.Request.Cookies[name] : new HttpCookie(name);
				HttpCookie cookie = new HttpCookie(name);
				cookie.HttpOnly = httpOnly;
				cookie.Secure = secure && CommonSettings.EnableSsl; // use secure cookie if we enable ssl				
				if (!string.IsNullOrWhiteSpace(domain))
				{
					cookie.Domain = domain;
				}
				cookie.Value = HttpUtility.UrlEncode(value);
				if (expires.HasValue) cookie.Expires = expires.Value;
				context.Response.Cookies.Set(cookie);
			}
		}

		public static void ClearCookie(this HttpContext context, string name)
		{
			context.Response.Cookies.Set(new HttpCookie(name) { Expires = DateTime.Now.AddDays(-1) });
		}
	}
}
