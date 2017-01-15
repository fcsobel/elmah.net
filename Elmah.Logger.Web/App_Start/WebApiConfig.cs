using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Elmah.Net.Logger.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services

			////Defaulting to [FromUri] for GET and HEAD requests
			//http://www.strathweb.com/2013/04/asp-net-web-api-parameter-binding-part-1-understanding-binding-from-uri/
			config.Services.Replace(typeof(IActionValueBinder), new CustomActionValueBinder());
			
			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

	/// <summary>
	/// Defaulting to [FromUri] for GET and HEAD requests
	/// http://www.strathweb.com/2013/04/asp-net-web-api-parameter-binding-part-1-understanding-binding-from-uri/
	/// </summary>
	public class CustomActionValueBinder : DefaultActionValueBinder
	{
		protected override HttpParameterBinding GetParameterBinding(HttpParameterDescriptor parameter)
		{
			// Use FromUri on GET and Head requests
			return parameter.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get)
				|| parameter.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Head) ?
					   parameter.BindWithAttribute(new FromUriAttribute()) : base.GetParameterBinding(parameter);
		}
	}
}
