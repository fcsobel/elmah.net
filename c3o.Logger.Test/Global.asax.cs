using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace c3o.Logger.Test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        void ErrorLog_Logged(object sender, ErrorLoggedEventArgs args)
        {
            //args.Entry.Error.ApplicationName = "Test"   ;

            //// send error to custom endpoint
            //args.Entry.Error.Exception.Data["ElmahId"] = args.Entry.Id;
            //var elmahIoLog = new Elmah.Io.ErrorLog(new Guid("da8ccd1a-da16-4ac6-a988-af6b506ab22d"), new Uri("http://localhost:59837/"));
            
            //elmahIoLog.Log(args.Entry.Error);

   //         new Guid("da8ccd1a-da16-4ac6-a988-af6b506ab22d"), new Uri("http://localhost:59837/")
        }

        
        void ErrorMail_Mailing(object sender, Elmah.ErrorMailEventArgs e)
        {
            int i = 0;
            i++;

        }

        protected virtual void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            int i = 0;
            i++;
        }

        protected virtual void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            int i = 0;
            i++;
        }

    }
}
