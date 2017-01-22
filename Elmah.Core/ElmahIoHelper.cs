using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.Client;
using Newtonsoft.Json;

namespace Elmah.Net
{
	public enum LogSeverity { Verbose = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Fatal = 5, }
    
    public class LogMessage
	{
		public string Application { get; set; }
		public DateTime DateTime { get; set; }
		public string Detail { get; set; }
		public LogSeverity? Severity { get; set; }
		public string Source { get; set; }				// 	System.Web.Mvc
		public string Title { get; set; }						
		public string Type { get; set; }				// System.Web.HttpException
		public string Url { get; set; }
		public List<Item> Data { get; set; }

        // check for key
		public bool Contains(string key)
		{
			return this.Data.Any(x => string.Compare(x.Key, key, true) == 0);
		}
		
		// Add or replace value for key
		public void Add(string key, string value)
		{
			var item = this.Data.Find(x => string.Compare(x.Key, key, true) == 0);

			if (item != null)
			{
				item.Value = value;
			}
			else
			{ 
				this.Data.Add(new Item() { Key = key, Value = value });
			}
		}

		public LogMessage()
		{
			this.DateTime = DateTime.UtcNow;
			this.Application = CommonSettings.Application;
			this.Data = new List<Item>();
		}

		public LogMessage(string source, string type, string title, string detail, LogSeverity severity = LogSeverity.Information)
			: this()
		{
			this.Source = source;
			this.Type = type;
			this.Title = title;
			this.Detail = detail;
			this.Severity = severity;
			//if (data != null) { this.Data = data; }
		}

		public class DataItem
		{
			public string Key { get; set; }
			public string Value { get; set; }
			public DataItem() { }
			public DataItem(string key, string value)
			{
				this.Key = key;
				this.Value = value;
			}
		}
	}



	public static class ElmahIoHelper
	{
		private static void Hydrate(this Message obj, LogMessage value)
		{
			obj.Application = value.Application;
			obj.DateTime = value.DateTime;
			obj.Detail = value.Detail;
			obj.Severity = (Severity)value.Severity;
			obj.Source = value.Source;
			obj.Title = value.Title;
			obj.Type = value.Type;
			obj.Data = value.Data.Select(x => new Item { Key = x.Key, Value = x.Value }).ToList();
			obj.Url = value.Url;
		}


		private static void LogMessage(Message message)
		{
			// create logger
			ILogger logger = new Logger(new Guid(ElmahIoSettings.Key), new Uri(ElmahIoSettings.Url));

			// log message
            logger.Log(message);
		}

		public static void LogToElmahIo(this LogMessage value)
		{
			// create message
			var message = new Message(value.Title);

			// load message
			message.Hydrate(value);

			// log message
			LogMessage(message);
		}


		public static void LogCustom(this Elmah.ErrorLogEntry entry, LogSeverity severity = LogSeverity.Error)
		{
			// set elmah id
			entry.Error.Exception.Data["ElmahId"] = entry.Id;

			// log entry
			LogCustom(entry.Error, severity);
		}

		/// <summary>
		/// Modified to send OUTER Source & Type. 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="severity"></param>
		public static void LogCustom(this Elmah.Error error, LogSeverity severity = LogSeverity.Error)
		{
			// fix application name
			error.ApplicationName = CommonSettings.Application;

			var message = new Message(error.Message)
			{
				Application = error.ApplicationName,
				Cookies = error.Cookies.AllKeys.Select(key => new Item {Key = key, Value = error.Cookies[key]}).ToList(),
				DateTime = error.Time,
				Detail = error.Detail,
				Form = error.Form.AllKeys.Select(key => new Item { Key = key, Value = error.Form[key] }).ToList(),
				Hostname = error.HostName,
				QueryString = error.QueryString.AllKeys.Select(key => new Item { Key = key, Value = error.QueryString[key] }).ToList(),
				ServerVariables = error.ServerVariables.AllKeys.Select(key => new Item { Key = key, Value = error.ServerVariables[key] }).ToList(),
				Title = error.Message,
				//Source = error.Source,
				Source = error.Exception.Source,
				StatusCode = error.StatusCode,
				//Type = error.Type,
				Type = error.Exception.GetType().ToString(),
				User = error.User,
				Data = error.Exception.ToDataList(),
			};
			LogMessage(message);
		}
		
		public static void LogToElmahIo(this Exception error, LogSeverity severity = LogSeverity.Error)
		{
			// map exception to message
			var message = new Message(error.Message) { 
				Severity = (Severity) severity,
				Application = CommonSettings.Application,
				//Detail = error.ToString(),
				Detail = error.ToString(),
                Data = error.ToDataList(),
				Source = error.Source,		
				Url = error.HelpLink,
				Type = error.GetType().ToString()
			};

			if (string.IsNullOrWhiteSpace(message.Application))
			{
				// set application name
				if (error.TargetSite != null && error.TargetSite.Module != null)
				{
					message.Application = error.TargetSite.Module.Name;
				}
			}

			if (string.IsNullOrWhiteSpace(message.Application))
			{
				message.Application = error.Source;
			}
		
			// log message
			LogMessage(message);
		}
	}
}