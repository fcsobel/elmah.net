using System;
using System.Web;
using Elmah;

namespace Elmah.Net
{
	public static class ElmahExtension
	{
        public static void LogToElmah(this Exception ex)
		{
			// recursively log AggregateExceptions
			if (ex is AggregateException)
			{
				AggregateException aggregate = (AggregateException)ex;

				foreach (var e in aggregate.InnerExceptions)
				{
					if (e != null)
					{
						e.LogToElmah();
					}
				}
			}
			else
			{
				if (HttpContext.Current != null)
				{
					ErrorSignal.FromCurrentContext().Raise(ex);
				}
				else
				{
					if (httpApplication == null) InitNoContext();
					ErrorSignal.Get(httpApplication).Raise(ex);
				}
			}
		}

		private static HttpApplication httpApplication = null;
		private static ErrorFilterConsole errorFilter = new ErrorFilterConsole();
		public static ErrorMailModule ErrorEmail = new ErrorMailModule();
		public static ErrorLogModule ErrorLog = new ErrorLogModule();
		public static ErrorTweetModule ErrorTweet = new ErrorTweetModule();

		private static void InitNoContext()
		{
			//httpApplication = new HttpApplication();
			httpApplication = new HttpApplication();
			errorFilter.Init(httpApplication);

			(ErrorEmail as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorEmail);

			(ErrorLog as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorLog);

			(ErrorTweet as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorTweet);
		}

		private class ErrorFilterConsole : ErrorFilterModule
		{
			public void HookFiltering(IExceptionFiltering module)
			{
				module.Filtering += new ExceptionFilterEventHandler(base.OnErrorModuleFiltering);
			}
		}	
	}
}