using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using c3o.Core;

namespace c3o.Logger.Web
{
	public enum HydrationLevel { Basic, Detailed }

    //public enum SearchSpan
    //{
    //    [Description("Five Minutes")]
    //    FiveMinutes = 5,
    //    [Description("Fifteen Minutes")]
    //    FifteenMinutes = 15,
    //    [Description("One Hour")]
    //    OneHour = 60,
    //    [Description("Two Hours")]
    //    TwoHours = 120,
    //    [Description("Three Hours")]
    //    ThreeHours = 180,
    //    [Description("Four Hours")]
    //    FourHours = 240,
    //    [Description("Six Hours")]
    //    SixHours = 360,
    //    [Description("Twelve Hours")]
    //    TwelveHours = 1260,
    //    [Description("Twenty Four Hours")]
    //    TwentyFourHours = 1440,
    //    [Description("Three Days")]
    //    ThreeDays = 4320,
    //    [Description("One Week")]
    //    OneWeek = 10080,
    //    [Description("All")]
    //    All = 0
    //}

    public class Item
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}

	public class LogObject
	{
		public long Id { get; set; }
		public string Name { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Icon { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Color { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        public LogObject() { }
		public LogObject(Data.INameId obj) : this() 
		{ 
			this.Id = obj.Id;
			this.Name = obj.Name;
		} 

		public LogObject(Data.LogMessageType obj)  
		{
			this.Id = obj.Id;
			this.Name = obj.Name;
			this.Icon = obj.Icon;
			this.Color = obj.Color;		
		}

        public LogObject(c3o.Logger.Data.SearchSpan span)
        {
            this.Id = (long)span;
            this.Name = span.ToString();
            this.Description = EnumHelper.GetEnumDescription(span);
        }
    }

	public class LogMessage
	{
		public long Id { get; set; }
		public DateTime DateTime { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Detail { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Hostname { get; set; }		
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Severity { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public int? StatusCode { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Title { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string Url { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string IpAddress { get; set; }

		public long LogId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? ApplicationId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? UserId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? MessageTypeId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? SourceId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? SessionId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? OrderId { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public long? PersonId { get; set; }

		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Elmah.Io.Client.Item> Form { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Elmah.Io.Client.Item> QueryString { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Elmah.Io.Client.Item> ServerVariables { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Elmah.Io.Client.Item> Cookies { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public List<Elmah.Io.Client.Item> Data { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public string HowLongAgo { get; set; }
						
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public LogObject Log { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public LogObject Application { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public LogObject MessageType { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public LogObject User { get; set; }
		[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
		public LogObject Source { get; set; }

		public LogMessage(c3o.Logger.Data.LogMessage obj, HydrationLevel level = HydrationLevel.Basic)
		{
			this.Id = obj.Id;
			this.DateTime = obj.DateTime;
			this.Hostname = obj.Hostname;
			this.Severity = obj.Severity.ToString();
			this.StatusCode = obj.StatusCode;
			this.Title = obj.Title;
			this.Url = obj.Url;
			this.IpAddress = obj.IpAddress;
			this.LogId = obj.LogId;
			this.ApplicationId = obj.ApplicationId;
			this.UserId = obj.UserId;
			this.MessageTypeId = obj.MessageTypeId;
			this.SourceId = obj.SourceId;
			this.HowLongAgo = obj.HowLongAgo;

			if (level == HydrationLevel.Detailed)
			{
				// get object detail
				if (obj.Log != null) this.Log = new LogObject(obj.Log);
				if (obj.Application != null) this.Application = new LogObject(obj.Application);
				if (obj.User != null) this.User = new LogObject(obj.User);
				if (obj.Source != null) this.Source = new LogObject(obj.Source);
				if (obj.MessageType != null) this.MessageType = new LogObject(obj.MessageType);

				if (obj.Detail != null)
				{
					this.Detail = obj.Detail.Content;
				}

				var data = obj.Original;
				if (data != null)
				{
					this.Form = data.Form;
					this.QueryString = data.QueryString;
					this.ServerVariables = data.ServerVariables;
					this.Cookies = data.Cookies;
					this.Data = data.Data;
				}
			}
		}
	}
}