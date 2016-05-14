using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Elmah.Io.Client;
using c3o.Logger.Data;

namespace c3o.Logger.Web
{
	[RoutePrefix("api.logger")]
	public class LoggerController : ApiController
	{
        protected SiteInstance Site { get; set; }
        protected LoggerContext db { get; set; }

        public LoggerController(SiteInstance site, LoggerContext loggerContext)
        {
            this.Site = site;
            this.db = loggerContext;
        }

        //[HttpPost]
        //[Route("messages")]
        //public LogMessage Update(LogMessage message)
        //{
        //    HydrationLevel level = HydrationLevel.Detailed;

        //    var obj = db.LogMessages.Where(x => x.Id == message.Id)
        //            .Include(x => x.Log)
        //            .Include(x => x.User)
        //            .Include(x => x.Source)
        //            .Include(x => x.MessageType)
        //            .Include(x => x.Application)
        //            .Include(x => x.Detail)
        //            .FirstOrDefault();

        //    return new LogMessage(obj, level);
        //}

        [HttpGet]
		[Route("messages/{id}")]
		public LogMessage Detail(long id, HydrationLevel level = HydrationLevel.Detailed)
		{
			//using (LoggerContext db = new LoggerContext())
			//{
				var message = db.LogMessages.Where(x => x.Id == id)
						.Include(x => x.Log)
						.Include(x => x.User)
						.Include(x => x.Source)
						.Include(x => x.MessageType)
						.Include(x => x.Application)
						.Include(x => x.Detail)
						.FirstOrDefault();

				return new LogMessage(message, level);
			//}			
		}

        [HttpPost]
        [Route("messages/filter/{id:alpha?}")]
        public LogSearchResponseModel Filter(string id = null
            , List<long> types = null
            , List<long> sources = null
            , List<long> users = null
            , DateTime? start = null
            , DateTime? end = null
            , SearchSpan span = SearchSpan.All
            , int limit = 10)
        {
            var filter = db.Filters.Where(x => x.Name == id)
                .Include(x=>x.FilterSources)
                .Include(x => x.FilterTypes)
                .FirstOrDefault();

            if (filter == null)
            {
                filter = new c3o.Logger.Data.Filter() { Name = id };
                db.Filters.Add(filter);
                db.SaveChanges();
            }
            else
            {
                filter.FilterTypes.Clear();
                filter.FilterSources.Clear();
            }

            filter.Limit = limit;
            filter.Span = span;
            filter.Start = start;
            filter.End = end;
            
            foreach (var key in types)
            {
                var item = db.MessageTypes.Find(key);
                if (item != null) { filter.FilterTypes.Add(item); }
            }

            foreach (var key in sources)
            {
                var item = db.MessageSources.Find(key);
                if (item != null) { filter.FilterSources.Add(item); }
            }

            db.SaveChanges();

            return null;
        }


        [HttpGet]
		[Route("messages/search/{log:alpha?}")]
		public LogSearchResponseModel Search(string log=null, string application = null
            //, Elmah.Io.Client.Severity? severity = null
            //, string type = null
            //, string source = null
            //,string user = null
            , List<long> logs = null
            , List<long> applications = null
            , List<Elmah.Io.Client.Severity> severities = null
            , List<long> types = null
			,List<long> sources = null
			,List<long> users = null
			,DateTime? start = null
			,DateTime? end = null
            , SearchSpan span = SearchSpan.All
            , int limit = 10
            , HydrationLevel level = HydrationLevel.Basic,
            string name = null)
        {
            //using (LoggerContext db = new LoggerContext())
            //{
            //if (this.Site.Sites.Any())
            //{
            //    foreach (var site in this.Site.Sites)
            //    {
            //        var test = site;
            //    }
            //}

            // save filter
            if (string.IsNullOrWhiteSpace(name)) { name = "Default"; }
            this.Filter(name, types, sources, users, start, end, span, limit);
            
            // convert to utc
            if (start.HasValue) start.Value.ToUniversalTime();
                if (end.HasValue) end.Value.ToUniversalTime();

                // convert to span
                if (!start.HasValue && span > 0)
                {
                    start = DateTime.UtcNow.AddMinutes((int)span * -1);
                }

                IQueryable<c3o.Logger.Data.LogMessage> messages = null;

                if (start.HasValue)
                {
                    if (end.HasValue)
                    {
                        messages = (IQueryable<c3o.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= start && x.DateTime <= end)
                            .Include(x => x.Log)
                            .Include(x => x.User)
                            .Include(x => x.Source)
                            .Include(x => x.MessageType)
                            .Include(x => x.Application)
                            .OrderByDescending(x => x.DateTime);
                    }
                    else
                    {
                        messages = (IQueryable<c3o.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= start)
                            .Include(x => x.Log)
                            .Include(x => x.User)
                            .Include(x => x.Source)
                            .Include(x => x.MessageType)
                            .Include(x => x.Application)
                            .OrderByDescending(x => x.DateTime);
                    }
                }
                else
                {
                    messages = (IQueryable<c3o.Logger.Data.LogMessage>)db.LogMessages
                            .Include(x => x.Log)
                            .Include(x => x.User)
                            .Include(x => x.Source)
                            .Include(x => x.MessageType)
                            .Include(x => x.Application)
                            .OrderByDescending(x => x.DateTime);
                }

                //Log theLog = null;
                if (!string.IsNullOrWhiteSpace(log))
                {
                    //var start = DateTime.Now.Date;
                    //var allLogs = (IQueryable<c3o.Logger.Data.Log>)db.Logs.OrderBy(x => x.Name);
                    var theLog = db.Logs.FirstOrDefault(x => x.Name == log);
                    if (theLog != null)
                    {
                        messages = messages.Where(x => x.LogId == theLog.Id);
                    }
                }

                if (logs != null && logs.Any())
                {
                    messages = messages.Where(x => logs.Any(y => y == x.LogId));
                }

                // application
                if (!string.IsNullOrWhiteSpace(application))
                {
                    var obj = db.LogApplications.FirstOrDefault(x => x.Name == application);
                    if (obj != null)
                    {
                        messages = messages.Where(x => x.ApplicationId == obj.Id);
                        //logs = logs.Where(x => x.Applications.Any(a => a.Id == obj.Id));						
                    }
                }

                // applications
                if (applications != null && applications.Any())
                {
                    //var list = types.Select(x => x.Id);
                    messages = messages.Where(x => applications.Any(y => y == x.ApplicationId));
                }

                //// types
                //if (!string.IsNullOrWhiteSpace(type))
                //{
                //    var obj = db.MessageTypes.FirstOrDefault(x => x.Name == type);
                //    if (obj != null)
                //    {
                //        messages = messages.Where(x => x.MessageTypeId == obj.Id);
                //    }
                //}

                if (types != null && types.Any())
                {
                    //var list = types.Select(x => x.Id);
                    messages = messages.Where(x => types.Any(y => y == x.MessageTypeId));
                }

                if (sources != null && sources.Any())
                {
                    //var list = sources.Select(x => x.Id);
                    messages = messages.Where(x => sources.Any(y => y == x.SourceId));
                }

                if (users != null && users.Any())
                {
                    //var list = users.Select(x => x.Id);
                    messages = messages.Where(x => users.Any(y => y == x.UserId));
                }

                //// sources
                //if (!string.IsNullOrWhiteSpace(source))
                //{
                //    var obj = db.MessageSources.FirstOrDefault(x => x.Name == source);
                //    if (obj != null)
                //    {
                //        messages = messages.Where(x => x.SourceId == obj.Id);
                //    }
                //}


                //// users
                //if (!string.IsNullOrWhiteSpace(user))
                //{
                //    var obj = db.Users.FirstOrDefault(x => x.Name == user);
                //    if (obj != null)
                //    {
                //        messages = messages.Where(x => x.UserId == obj.Id);
                //    }
                //}

                // severities
                if (severities != null && severities.Any())
                {
                    //var list = users.Select(x => x.Id);
                    messages = messages.Where(x => severities.Any(y => y == x.Severity));
                }

                // severity
                //if (severity.HasValue) { messages = messages.Where(x => x.Severity == severity); }

                if (limit > 0)
                {
                    messages = messages.Take(limit);
                }

                var filters = db.Filters.Include(x => x.FilterSources).Include(x => x.FilterTypes).ToList();

                // Setup model
                var model = new LogSearchResponseModel(messages.ToList(), filters, level);
    //            if (theLog != null) { model.Log = theLog.Name; }
				//model.Application = application;
				//model.Severity = severity;
				//model.Source = source;
				//model.Span = span;
				//model.Type = type;
				//model.User = user;
				return model;
			}
		//}
	}
}