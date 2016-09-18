using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.IO;
using c3o.Logger.Data;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web;
using System.Reflection;
using SimpleInjector.Integration.Web.Mvc;
//using c3o.Site.Data;
using Elmah.Contrib.WebApi;

namespace c3o.Logger.Web
{
    public class Global : HttpApplication
    {
        //private SiteInstance SiteInstance { get; set; }

        void Application_Beginrequest(object sender, EventArgs e)
        {
            //Container cont = (Container) GlobalConfiguration.Configuration.DependencyResolver;

            //using (cont.BeginExecutionContextScope())
            //{
            //    //var initializer = cont.GetInstance<MyDbInitializer>();
            //    //intializer.InitializeDatabase();
            //    var site = (SiteInstance)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SiteInstance));
            //    var list = site.Sites;

            //}

            //var site = SiteContext.Current.Site;
            //var site = container.GetInstance<SiteInstance>();
            //var list = this.SiteInstance.Sites;

            // Get a PostAuthenticateRequestProvider and use this to apply a 
            // correctly configured principal to the current http request
            //var site = (SiteInstance) DependencyResolver.Current.GetService(typeof(SiteInstance));
            //var list = site.Sites;

            //var site = (SiteInstance)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SiteInstance));
            //var list = site.Sites;

            //var site2 = (SiteContext)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SiteContext));
            //var current = site2.Site;

            //provider.ApplyPrincipleToHttpRequest(HttpContext.Current);
        }


        void Application_Start(object sender, EventArgs e)
        {
            //// Code that runs on application startup
            //GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            // Simple Injector
            // Create the container as usual.
            var container = new Container();            
            //container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

			// Register your types, for instance using the scoped lifestyle:
			//container.Register<SiteContext, SiteContext>(Lifestyle.Scoped);
			//container.Register<SiteList>(Lifestyle.Singleton);
			//container.Register<SiteContext>(Lifestyle.Scoped);
			////container.Register<SiteInstance>(Lifestyle.Scoped);
					

			// single instance
			//container.Register<ISiteInstance, SiteInstanceBase>(Lifestyle.Scoped);

			// Multiple instance
			container.Register<ISiteInstance, SiteInstance>(Lifestyle.Scoped);

			container.Register<LoggerContext>(Lifestyle.Scoped);
			//container.Register<SiteInstance>(Lifestyle.Singleton);

			SimpleInjectorConfig.Register(container);

			//WEB API: This is an extension method from the integration package.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            
            //MVC: This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //MVC: This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();
			
            //container.Verify();

            //WEB API Dependency Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            //MVC Dependency Resolver
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            //this.SiteInstance = container.GetInstance<SiteInstance>();

            // usual Web API configuration stuff.
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // MVC Routes
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            // ELMAH Web API Contrib
            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());


            // Add formatters (CamelCasePropertyNamesContractResolver..etc)
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

            //// Update DB for each site in list
            //var site = (SiteList)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SiteList));
            //var db = (SiteContext)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SiteContext));

            ////site.Refresh(db);
            //site.Sites = db.Sites.ToList().Select(x => new SiteRecord(x.Name, x.Name)).ToList();

            //var list = site.Sites;
            //foreach (var item in list)
            //{
            //    item.CheckSiteDb();
            //}
        }
    }
}