using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using WebApiContrib.Formatting.Jsonp;

namespace c3o.Logger.Web
{
	public class FormatterConfig
	{
		public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
		{

			// Remove the XML formatter
			//formatters.Remove(formatters.XmlFormatter);

			// Remove the JSON formatter
		    //formatters.Remove(formatters.JsonFormatter);				    

			// Set json as default for text/html: 
			//url: http://stackoverflow.com/questions/9847564/how-do-i-get-asp-net-web-api-to-return-json-instead-of-xml-using-chrome
			formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			// Camel Casing
			// To write JSON property names with camel casing, without changing your data model, set the CamelCasePropertyNamesContractResolver on the serializer: 
			// url: http://www.asp.net/web-api/overview/formats-and-model-binding/json-and-xml-serialization			
			var jsonFormatter = formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			//jsonFormatter.SerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

			// url: https://github.com/WebApiContrib/WebApiContrib.Formatting.Jsonp
			//var jsonFormatter = formatters.JsonFormatter;
			// Insert the JSONP formatter in front of the standard JSON formatter. (Why?)
			// URL: https://dataguidance.codeplex.com/discussions/443135
			//var jsonpFormatter = new JsonpMediaTypeFormatter(formatters.JsonFormatter);
			//formatters.Insert(0, jsonpFormatter);

			//// URL: http://www.asp.net/web-api/overview/formats-and-model-binding/json-and-xml-serialization
			//// To preserve object references in JSON - The serializer detects that the when properties create a loop and replace the value with an object reference: {"$ref":"1"}.
			//// WARNING: Object references are not standard in JSON and your clients may not be able to parse the results. It might be better simply to remove cycles from the graph. 			
			//jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
		}
	}
}