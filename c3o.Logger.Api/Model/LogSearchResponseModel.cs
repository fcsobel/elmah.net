using System;
using System.Collections.Generic;
using System.Linq;
using c3o.Core;

namespace c3o.Logger.Web
{
    public class LogSearchResponseModel
	{
		//public string Log { get; set; }
		//public string Application { get; set; }
		//public string Type { get; set; }
		//public string Source { get; set; }
		//public Elmah.Io.Client.Severity? Severity { get; set; }
		//public string User { get; set; }
		//public SearchSpan Span { get; set; }

		//public List<string> Severities { get; set; }
		public List<LogMessage> Messages { get; set; }

		public List<LogObject> Severities { get; set; }
		public List<LogObject> Logs { get; set; }
		public List<LogObject> Applications { get; set; }
		public List<LogObject> Users { get; set; }
		public List<LogObject> Types { get; set; }
		public List<LogObject> Sources { get; set; }
		public List<LogObject> Spans { get; set; }

		public LogSearchResponseModel() {
			this.Messages = new List<LogMessage>();
			this.Logs = new List<LogObject>();
			this.Applications = new List<LogObject>();
			this.Users = new List<LogObject>();
			this.Types = new List<LogObject>();
			this.Sources = new List<LogObject>();			
		}

		public LogSearchResponseModel(List<c3o.Logger.Data.LogMessage> list, HydrationLevel level = HydrationLevel.Basic) : this()
		{
			this.Logs =				list.Where(x=>x.Log != null).Select(x => x.Log).Distinct().Select(y => new LogObject(y)).ToList();
			this.Applications =		list.Where(x=>x.Application != null).Select(x => x.Application).Distinct().Select(y => new LogObject(y)).ToList();
			this.Users =			list.Where(x=>x.User != null).Select(x => x.User).Distinct().Select(y => new LogObject(y)).ToList();
			this.Types =			list.Where(x=>x.MessageType != null).Select(x => x.MessageType).Distinct().Select(y => new LogObject(y)).ToList();
			this.Sources =			list.Where(x=>x.Source != null).Select(x => x.Source).Distinct().Select(y => new LogObject(y)).ToList();
			this.Severities =		list.Select(x=>x.Severity).Distinct().Select(y => new LogObject { Id = 0, Name = y.ToString() }).ToList();
			//this.Spans =			EnumHelper.GetValues<SearchSpan>().Select(y => new LogObject { Id = (long)y, Name = y.ToString() }).ToList();
            this.Spans = EnumHelper.GetValues<SearchSpan>().Select(y => new LogObject(y)).ToList();
            foreach (var item in list)
			{
				this.Messages.Add(new LogMessage(item));
			}
		}
	}
}