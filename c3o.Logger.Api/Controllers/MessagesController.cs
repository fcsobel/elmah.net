using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Elmah.Io.Client;
using c3o.Core;
using c3o.Logger.Data;

namespace c3o.Logger.Web
{
	[RoutePrefix("api/v2/messages")]
	public class MessagesController : ApiController
	{
        protected LoggerContext db { get; set; }

        public MessagesController(LoggerContext loggerContext) : base()
        {
            this.db = loggerContext;                    
        }

		[HttpPost]
		[Route("")]
		public IHttpActionResult Post(Message message, string logId)
		{
            // Clean up Source - remove dynamic name after .cshtml.
            // App_Web_attractionorderboxv3.cshtml.89207646.iyumqbn_
            if (!string.IsNullOrWhiteSpace(message.Source))
            {
                if (message.Source.Contains(".cshtml."))
                {
                    message.Source = message.Source.Substring(0, message.Source.IndexOf(".cshtml.") + ".cshtml".Length);
                }
            }

            // Fix Application
            if (string.IsNullOrWhiteSpace(message.Application)) message.Application = message.ServerVariables.GetValue("HTTP_HOST");
            if (string.IsNullOrWhiteSpace(message.Application)) message.Application = message.Hostname;

            // Fix Url
            if (string.IsNullOrWhiteSpace(message.Url)) { message.Url = message.Data.GetValue("Url"); }
            if (string.IsNullOrWhiteSpace(message.Url))
            {
                string host = message.ServerVariables.GetValue("HTTP_HOST");
                if (!string.IsNullOrWhiteSpace(host))
                {
                    string url = message.ServerVariables.GetValue("URL");
                    if (message.ServerVariables.GetValue("HTTPS") == "off")
                    {
                        message.Url = string.Format("http://{0}{1}", host, url);
                    }
                    else
                    {
                        message.Url = string.Format("https://{0}{1}", host, url);
                    }
                }
            }

            // save message		
           // using (LoggerContext db = new LoggerContext())
			{
				if (!string.IsNullOrWhiteSpace(logId))
				{
					var entry = new c3o.Logger.Data.LogMessage(message, logId);

					// map log
					var key = new Guid(logId);
					entry.Log = db.Logs.Where(x => x.LogId == key).Include(x=>x.Applications).FirstOrDefault() ?? entry.Log;

					if (entry.Log != null)
					{
						// map application
						if (entry.Log.Applications.Any())
						{
							entry.Application = entry.Log.Applications.Where(x => x.Name == message.Application).FirstOrDefault() ?? entry.Application;
						}
					}

					// always point application at log
					entry.Application.Log = entry.Log;

					// map user
					if (!string.IsNullOrWhiteSpace(message.User)) { entry.User = db.Users.FirstOrDefault(x => x.Name == message.User) ?? entry.User; }

					// map source
					if (!string.IsNullOrWhiteSpace(message.Source)) { entry.Source = db.MessageSources.FirstOrDefault(x => x.Name == message.Source) ?? entry.Source; }

					// map type
					if (!string.IsNullOrWhiteSpace(message.Type)) { entry.MessageType = db.MessageTypes.FirstOrDefault(x => x.Name == message.Type) ?? entry.MessageType; }
					
					//// map detail
					if (entry.Detail != null) 
					{ 
						// find matching detail by checksum key.  Then by Detail
						entry.Detail = db.MessageDetails.Where(x => x.ContentKey == entry.Detail.ContentKey)
							.ToList()
							.FirstOrDefault(x => x.Content == message.Detail) ?? entry.Detail; 
					}

                    // Location: https://elmah.io/api/v2/messages?id=6707A1B0A79C8E85&logid=5082a1ce-c234-4c2e-92d4-5c5bd5a72854
                    string url = string.Format("{0}api/v2/messages?id={1}&logid={2}", ElmahIoSettings.Url, entry.Id, entry.Log.LogId);

                    if (entry.Log != null && entry.Log.Id > 0) entry.LogId = entry.Log.Id;
                    if (entry.Application != null && entry.Application.Id > 0) entry.ApplicationId = entry.Application.Id;
                    if (entry.Detail != null && entry.Detail.Id > 0) entry.DetailId = entry.Detail.Id;
                    if (entry.MessageType != null && entry.MessageType.Id > 0) entry.MessageTypeId = entry.MessageType.Id;
                    if (entry.Source != null && entry.Source.Id > 0) entry.SourceId = entry.Source.Id;
                    if (entry.User != null && entry.User.Id > 0) entry.UserId = entry.User.Id;

                    // see if we already have a matching message
                    if (entry.Severity == Data.LogSeverity.Information
                        && entry.ElmahId == null
                        && entry.Hostname == null
                        && entry.StatusCode == null)
                    {
                        //TODO: Add Indexes to optimize this
                        // check for matching entry in the last 10 minutes
                        var limit = entry.DateTime.AddMinutes(-10);
                        var match = db.LogMessages.Where(
                            x => x.Severity == entry.Severity
                            && x.StatusCode == null
                            && x.Hostname == null
                            && x.ElmahId == null
                            && x.Title == entry.Title
                            && x.Url == entry.Url
                            && x.Blob == entry.Blob
                            && x.LogId == entry.LogId
                            && x.ApplicationId == entry.ApplicationId
                            && x.UserId == entry.UserId
                            && x.MessageTypeId == entry.MessageTypeId
                            && x.SourceId == entry.SourceId
                            && x.IpAddressId == entry.IpAddressId
                            && x.DetailId == entry.DetailId
                            && limit < x.DateTime
                            ).FirstOrDefault();
                        if (match != null)
                        {
                            match.LogCount = match.LogCount + 1;
                            match.DateTime = entry.DateTime;
                            db.SaveChanges();
                            return Created(url, "");
                        }
                    }

                    // Add new log entry
                    db.LogMessages.Add(entry);
					db.SaveChanges();

					// created
					return Created(url, "");
				}

				// not created
				return Ok();
			}
			//return "ok";
		}

		
		[HttpGet]
		[Route("")]
		public LogMessage Get(string logId, string id)
		{
			// get message
			//using (LoggerContext db = new LoggerContext())
			//{
				if (!string.IsNullOrWhiteSpace(logId))
				{
					// find log
					var key = new Guid(logId);
					var log = db.Logs.Where(x => x.LogId == key).FirstOrDefault();

					if (log != null)
					{
						var messageId = id.ParseLong();
						if (messageId > 0)
						{
							var message = db.LogMessages.Where(x => x.Id == messageId)
								.Include(x => x.Log)
								.Include(x => x.User)
								.Include(x => x.Source)
								.Include(x => x.MessageType)
								.Include(x => x.Application)
								.FirstOrDefault();

							return new LogMessage(message, HydrationLevel.Detailed);
						}
					}
				}
			//}

			return null;			
		}

		[HttpGet]
		[Route("")]
		public MessagesResult  Get(string logId, int pageIndex, int pageSize)
		{
			//return null;
			//return string.Format("test pageIndex {0} pageSize {1}", pageIndex, pageSize);
			var model = new MessagesResult();
			model.Total = pageSize;
			return model;
		}
	}
}
