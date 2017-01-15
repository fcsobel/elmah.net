using System;
using System.Collections.Generic;
using System.Linq;
using Elmah.Net;

namespace Elmah.Net.Logger.Web
{
    public class Filter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Distribution { get; set; }
        public Data.Query Query { get; set; }

        public Filter()
        {
            this.Query = new Data.Query();
        }

        public Filter(Elmah.Net.Logger.Data.Filter obj): this()
        {
            this.Id = obj.Id;
            this.Name = obj.Name;
            this.Title = obj.Title;
            this.Description = obj.Description;
            this.Distribution = obj.Distribution;
            this.Query = obj.Query;
        }
    }

	public class LogCount
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public int Count { get; set; }
	}
    
    public class LogSearchResponseModel
	{
        public Data.Query Query { get; set; }
        //public string Log { get; set; }
        //public string Application { get; set; }
        //public string Type { get; set; }
        //public string Source { get; set; }
        //public Elmah.Io.Client.Severity? Severity { get; set; }
        //public string User { get; set; }
        //public SearchSpan Span { get; set; }
        //public List<string> Severities { get; set; }
        public List<LogMessage> Messages { get; set; }
		public List<LogObject> Logs { get; set; }
		public List<LogObject> Applications { get; set; }
		public List<LogObject> Types { get; set; }
		public List<LogObject> Sources { get; set; }
        public List<LogObject> Severities { get; set; }
        public List<LogObject> Users { get; set; }
        public List<LogObject> Spans { get; set; }
        public List<Filter> Filters { get; set; }

		public List<LogCount> TypeCount { get; set; }
		public List<LogCount> TypeCount2 { get; set; }

		public LogSearchResponseModel() {
			this.Messages = new List<LogMessage>();
			this.Logs = new List<LogObject>();
			this.Applications = new List<LogObject>();
			this.Users = new List<LogObject>();
			this.Types = new List<LogObject>();
			this.Sources = new List<LogObject>();
            this.Filters = new List<Filter>();
		}

		public LogSearchResponseModel(Data.Query query, List<Elmah.Net.Logger.Data.LogMessage> list, List<Elmah.Net.Logger.Data.Filter> filters, HydrationLevel level = HydrationLevel.Basic) : this()
		{
            this.Query = query;
            this.Logs =				list.Where(x=>x.Log != null).Select(x => x.Log).Distinct().Select(y => new LogObject(y)).ToList();
			this.Applications =		list.Where(x=>x.Application != null).Select(x => x.Application).Distinct().Select(y => new LogObject(y)).ToList();
			this.Users =			list.Where(x=>x.User != null).Select(x => x.User).Distinct().Select(y => new LogObject(y)).ToList();
			this.Types =			list.Where(x=>x.MessageType != null).Select(x => x.MessageType).Distinct().Select(y => new LogObject(y)).ToList();
			this.Sources =			list.Where(x=>x.Source != null).Select(x => x.Source).Distinct().Select(y => new LogObject(y)).ToList();
			this.Severities =		list.Select(x=>x.Severity).Distinct().Select(y => new LogObject { Id = 0, Name = y.ToString() }).ToList();
            this.Spans =            EnumHelper.GetValues<Elmah.Net.Logger.Data.SearchSpan>().Select(y => new LogObject(y)).ToList();

            // load messages
            foreach (var item in list)
			{
				this.Messages.Add(new LogMessage(item));
			}

            if (filters != null)
            {
                // load filters
                foreach (var item in filters)
                {
                    this.Filters.Add(new Filter(item));
                }
            }
        }
	}
}