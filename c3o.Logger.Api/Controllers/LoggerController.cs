using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Elmah.Io.Client;
using c3o.Logger.Data;
using c3o.Core;

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

        [HttpDelete]
        [Route("messages/{id}")]
        public LogMessage Delete(long id)
        {
            //using (LoggerContext db = new LoggerContext())
            //{
            var message = db.LogMessages.Where(x => x.Id == id).FirstOrDefault();

            db.LogMessages.Remove(message);
            db.SaveChanges();

            return new LogMessage(message, HydrationLevel.Basic);
            //}
        }


        [HttpGet]
        [Route("messages/init")]
        public LogSearchResponseModel Init()
        {
            LogSearchResponseModel model = new LogSearchResponseModel();
            // using (LoggerContext db = new LoggerContext())
            //{
            model.Logs = db.Logs.ToList().Select(y => new LogObject(y)).ToList();
            model.Applications = db.LogApplications.ToList().Select(y => new LogObject(y)).ToList();
            //model.Users = db.Users.Select(y => new LogObject(y)).ToList();
            model.Types = db.MessageTypes.ToList().Select(y => new LogObject(y)).ToList();
            model.Sources = db.MessageSources.ToList().Select(y => new LogObject(y)).ToList();
            model.Severities = EnumHelper.GetValues<LogSeverity>().Select(y => new LogObject(y)).ToList();
            model.Spans = EnumHelper.GetValues<SearchSpan>().Select(y => new LogObject(y)).ToList();
            //}
            return model;
        }

        /// <summary>
        /// Search and Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("messages/search/{id}")]
        public LogSearchResponseModel Search(string id, Query query)
        {


            var filter = db.Filters.Where(x => x.Name == id).FirstOrDefault();
            if (filter == null)
            {
                filter = new c3o.Logger.Data.Filter() { Name = id };
                db.Filters.Add(filter);
            }
            filter.Query = query;
            db.SaveChanges();

            return Search(filter.Query);

        }

        /// <summary>
        /// Search By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("messages/search/{name}")]
        public LogSearchResponseModel SearchByName(string name)
        {
            var filter = db.Filters.Where(x => x.Name == name).FirstOrDefault();
            if (filter != null)
            {
                return Search(filter.Query);
            }
            return null;
        }

        /// <summary>
        /// Delete Search By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("messages/search/{name}")]
        public LogSearchResponseModel DeleteByName(string name)
        {
            var filter = db.Filters.Where(x => x.Name == name).FirstOrDefault();
            if (filter != null)
            {
                db.Filters.Remove(filter);
                db.SaveChanges();
            }
            return Search(new Query());
        }



        /// <summary>
        /// Search
        /// </summary>
        /// <param name="query"></param>
        /// <param name="level"></param>
        /// <param name="log"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("messages/search")]
        public LogSearchResponseModel Search(Query query, HydrationLevel level = HydrationLevel.Basic, string log = null, string application = null)
        {
            // convert to utc
            if (query.Start.HasValue) query.Start.Value.ToUniversalTime();
            if (query.End.HasValue) query.End.Value.ToUniversalTime();

            // convert to span
            if (!query.Start.HasValue && query.Span > 0)
            {
                query.Start = DateTime.UtcNow.AddMinutes((int)query.Span * -1);
            }

            IQueryable<c3o.Logger.Data.LogMessage> messages = null;

            if (query.Start.HasValue)
            {
                if (query.End.HasValue)
                {
                    messages = (IQueryable<c3o.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= query.Start && x.DateTime <= query.End)
                        .Include(x => x.Log)
                        .Include(x => x.User)
                        .Include(x => x.Source)
                        .Include(x => x.MessageType)
                        .Include(x => x.Application)
                        .OrderByDescending(x => x.DateTime);
                }
                else
                {
                    messages = (IQueryable<c3o.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= query.Start)
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

            if (query.Logs != null && query.Logs.Any())
            {
                messages = messages.Where(x => query.Logs.Any(y => y == x.LogId));
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
            if (query.Applications != null && query.Applications.Any())
            {
                //var list = types.Select(x => x.Id);
                messages = messages.Where(x => query.Applications.Any(y => y == x.ApplicationId));
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

            if (query.Types != null && query.Types.Any())
            {
                //var list = types.Select(x => x.Id);
                messages = messages.Where(x => query.Types.Any(y => y == x.MessageTypeId));
            }

            if (query.Sources != null && query.Sources.Any())
            {
                //var list = sources.Select(x => x.Id);
                messages = messages.Where(x => query.Sources.Any(y => y == x.SourceId));
            }

            if (query.Users != null && query.Users.Any())
            {
                //var list = users.Select(x => x.Id);
                messages = messages.Where(x => query.Users.Any(y => y == x.UserId));
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
            if (query.Severities != null && query.Severities.Any())
            {
                //var list = users.Select(x => x.Id);
                messages = messages.Where(x => query.Severities.Any(y => y == x.Severity));
            }

            // severity
            //if (severity.HasValue) { messages = messages.Where(x => x.Severity == severity); }

            if (query.Limit > 0)
            {
                messages = messages.Take(query.Limit);
            }

            // get filters
            var filters = db.Filters.OrderBy(x => x.Name).ToList();

            // Setup model
            var model = new LogSearchResponseModel(query, messages.ToList(), filters, level);

            // Make sure Query Items are in lists!
            foreach (var type in query.Types)
            {
                if (!model.Types.Any(x => x.Id == type))
                {
                    var obj = db.MessageTypes.Find(type);
                    if (obj != null) { model.Types.Add(new LogObject(obj)); }
                }
            }
            foreach (var key in query.Logs)
            {
                if (!model.Logs.Any(x => x.Id == key))
                {
                    var obj = db.Logs.Find(key);
                    if (obj != null) { model.Logs.Add(new LogObject(obj)); }
                }
            }
            foreach (var key in query.Applications)
            {
                if (!model.Applications.Any(x => x.Id == key))
                {
                    var obj = db.LogApplications.Find(key);
                    if (obj != null) { model.Applications.Add(new LogObject(obj)); }
                }
            }
            foreach (var key in query.Sources)
            {
                if (!model.Sources.Any(x => x.Id == key))
                {
                    var obj = db.MessageSources.Find(key);
                    if (obj != null) { model.Sources.Add(new LogObject(obj)); }
                }
            }
            foreach (var key in query.Users)
            {
                if (!model.Users.Any(x => x.Id == key))
                {
                    var obj = db.Users.Find(key);
                    if (obj != null) { model.Users.Add(new LogObject(obj)); }
                }
            }
            foreach (var key in query.Severities)
            {
                if (!model.Severities.Any(x => x.Id == (long)key))
                {
                    model.Severities.Add(new LogObject(key));
                }
            }

            return model;
        }
    }
}