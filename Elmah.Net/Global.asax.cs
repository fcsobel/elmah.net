using c3o.Logger.Data;
using c3o.Logger.Web;
using Elmah.Contrib.WebApi;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace c3o.Logger.Demo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			// Simple Injector
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
			container.Register<ISiteInstance, SiteInstanceBase>(Lifestyle.Scoped);
			container.Register<LoggerContext>(Lifestyle.Scoped);

			//WEB API: This is an extension method from the integration package.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			//container.Verify();

			//WEB API Dependency Resolver
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			// usual Web API configuration stuff.
			//AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);

			//MVC Filters
			//FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			// MVC Routes
			//RouteConfig.RegisterRoutes(RouteTable.Routes);

			// ELMAH Web API Contrib
			GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

			// Add formatters (CamelCasePropertyNamesContractResolver..etc)
			FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
        }
    }
}
