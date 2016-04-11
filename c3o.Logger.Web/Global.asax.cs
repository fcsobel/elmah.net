using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using DbUp;
using System.IO;

namespace c3o.Logger.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Init Database
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["LoggerContext"];

            if (connection != null)
            {
                var path = Server.MapPath("~/_deploy/sql");

                if (Directory.Exists(path))
                {
                    // configure 
                    var upgrader = DeployChanges.To
                            .SqlDatabase(connection.ConnectionString)
                            .WithScriptsFromFileSystem(Server.MapPath("~/_deploy/sql"))
                            //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            .LogToConsole()
                            .Build();
                    // setup db
                    upgrader.PerformUpgrade();
                }
            }

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Add formatters (CamelCasePropertyNamesContractResolver..etc)
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
        }
    }
}