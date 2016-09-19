using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Elmah.Io.Client;
using Newtonsoft.Json;
using c3o.Core;

namespace c3o.Logger.Data 
{
	//public enum LogSeverity { Verbose = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Fatal = 5, }

    public class LogMessage 
	{
		public long Id { get; set; }
		public string ElmahId { get; set; }
		public DateTime DateTime { get; set; }
        public string Hostname { get; set; }		
		public LogSeverity Severity { get; set; }
        public int? StatusCode { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string Blob { get; set; }
		public long? IpAddressId { get; set; }
        public long LogId { get; set; }
        public long? ApplicationId { get; set; }
        public long? UserId { get; set; }
        public long? MessageTypeId { get; set; }
        public long? SourceId { get; set; }
        public long? DetailId { get; set; }
        public int LogCount { get; set; }

        public Log Log { get; set; }
		public LogApplication Application { get; set; }
		public LogUser User { get; set; }
		public LogMessageType MessageType { get; set; }
		public LogMessageSource Source { get; set; }		
		public LogMessageDetail Detail { get; set; }

        [NotMapped]
        public string IpAddress
        {
            get { return this.IpAddressId == null ? null : NetworkHelper.IPv4Int64ToString(this.IpAddressId.Value); }
            set { this.IpAddressId = NetworkHelper.IPv4StringToInt64(value); }
        }

        [NotMapped]
		public string HowLongAgo { get { return this.DateTime.GetDelta(); } }

		[NotMapped]
		public LogMessageBlob Original { get { return JsonConvert.DeserializeObject<LogMessageBlob>(this.Blob); } }

		public LogMessage()
        {
            this.LogCount = 1;
        }
		public LogMessage(Message obj, string logId) : this()
        {
			this.ElmahId = obj.Id;
			if (string.IsNullOrWhiteSpace(this.ElmahId)) { this.ElmahId = obj.Data.GetValue("ElmahId"); }
			
			this.DateTime = DateTime.UtcNow; // Use UTC server dt
			this.Hostname = obj.Hostname;
            this.Severity = (LogSeverity)(obj.Severity ?? Elmah.Io.Client.Severity.Error);
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
