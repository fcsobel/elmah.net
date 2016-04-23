using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Elmah.Io.Client;
using Newtonsoft.Json;
using c3o.Core;

namespace c3o.Logger.Data 
{
	public class LogMessageBlob
	{
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Item> Form { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Item> QueryString { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Item> ServerVariables { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Item> Cookies { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Item> Data { get; set; }

		// don't serialize the Manager property if an employee is their own manager
		public bool ShouldSerializeForm() { return (this.Form != null && this.Form.Any()); }
		public bool ShouldSerializeQueryString() { return (this.QueryString != null && this.QueryString.Any()); }
		public bool ShouldSerializeServerVariables() { return (this.ServerVariables != null && this.ServerVariables.Any()); }
		public bool ShouldSerializeCookies() { return (this.Cookies != null && this.Cookies.Any()); }
		public bool ShouldSerializeData() { return (this.Data != null && this.Data.Any()); }

		public LogMessageBlob() {
			this.Form = new List<Item>();
			this.QueryString = new List<Item>();
			this.ServerVariables = new List<Item>();
			this.Cookies = new List<Item>();
			this.Data = new List<Item>();
		}

		public LogMessageBlob(Message value) : this()
		{
			this.Form = value.Form;
			this.QueryString = value.QueryString;
			this.ServerVariables = value.ServerVariables;
			this.Cookies = value.Cookies;
			this.Data = value.Data;
		}
	}

	public class LogMessage 
	{
		public long Id { get; set; }
		public string ElmahId { get; set; }
		public DateTime DateTime { get; set; }
        public string Hostname { get; set; }		
		public Severity Severity { get; set; }
        public int? StatusCode { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string Blob { get; set; }

		public long? IpAddressId { get; set; }		
		[NotMapped]
		public string IpAddress 
		{
			get { return this.IpAddressId == null ? null : NetworkHelper.IPv4Int64ToString(this.IpAddressId.Value); }
			set { this.IpAddressId = NetworkHelper.IPv4StringToInt64(value); }
		}		

		public long LogId { get; set; }
		public Log Log { get; set; }

		public long? ApplicationId { get; set; }
		public LogApplication Application { get; set; }
		
		public long? UserId { get; set; }
		public LogUser User { get; set; }

		public long? MessageTypeId { get; set; }
		public LogMessageType MessageType { get; set; }

		public long? SourceId { get; set; }
		public LogMessageSource Source { get; set; }

		public long? DetailId { get; set; }
		public LogMessageDetail Detail { get; set; }		

		//public List<Item> Form { get; set; }
		//public List<Item> QueryString { get; set; }
		//public List<Item> ServerVariables { get; set; }
		//public List<Item> Cookies { get; set; }
		//public List<Item> Data { get; set; }

		[NotMapped]
		public string HowLongAgo { get { return this.DateTime.GetDelta(); } }

		[NotMapped]
		public LogMessageBlob Original { get { return JsonConvert.DeserializeObject<LogMessageBlob>(this.Blob); } }

		public LogMessage() { }
		public LogMessage(Message obj, string logId) 
		{
			this.ElmahId = obj.Id;
			if (string.IsNullOrWhiteSpace(this.ElmahId)) { this.ElmahId = obj.Data.GetValue("ElmahId"); }
			
			//this.DateTime = obj.DateTime;
			this.DateTime = DateTime.UtcNow; // Use UTC server dt
			this.Hostname = obj.Hostname;
			this.Severity = obj.Severity ?? Elmah.Io.Client.Severity.Error;
			this.StatusCode = obj.StatusCode;
			this.Title = obj.Title;
			
			this.Url = obj.Url;
		    //if (string.IsNullOrWhiteSpace(this.Url)) { this.Url = obj.Data.GetValue("Url"); }
			//if (string.IsNullOrWhiteSpace(this.Url)) { this.Url = obj.ServerVariables.GetValue("Url"); }
			
			var ip = obj.ServerVariables.GetValue("REMOTE_ADDR");
			if (!string.IsNullOrWhiteSpace(ip))
			{
				this.IpAddress = ip;
			}

			//this.Type = obj.Type;
			this.Blob = JsonConvert.SerializeObject(new LogMessageBlob(obj));

			// log & application
			if (!string.IsNullOrWhiteSpace(obj.Application))
			{
				if (this.Log == null)
				{
					this.Log = new Log() { LogId = new Guid(logId), Name = obj.Application };
				}
				if (this.Application == null)
				{
					this.Application = new LogApplication() { Name = obj.Application };
				}				
			}

			// source
			if (!string.IsNullOrWhiteSpace(obj.Source))
			{
				if (this.Source == null)
				{
					this.Source = new LogMessageSource { Name = obj.Source };
				}
			}

			// type
			if (!string.IsNullOrWhiteSpace(obj.Type))
			{
				if (this.MessageType == null)
				{
					this.MessageType = new LogMessageType { Name = obj.Type };
				}
			}			

			// user
			if (!string.IsNullOrWhiteSpace(obj.User))
			{
				if (this.User == null)
				{
					this.User = new LogUser() { Name = obj.User };
				}
			}
			
			// detail
			if (!string.IsNullOrWhiteSpace(obj.Detail))
			{
				if (this.Detail == null)
				{
					this.Detail = new LogMessageDetail() { 
						Content = obj.Detail,
						ContentKey = StringHelper.CheckSum(obj.Detail)
					};
				}
			}			
		}
	}
}
