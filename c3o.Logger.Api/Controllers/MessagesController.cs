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
		[HttpPost]
		[Route("")]
		public IHttpActionResult Post(Message message, string logId)
		{
            // Fix Application
            if (string.IsNullOrWhiteSpace(message.Application)) message.Application = message.ServerVariables.GetValue("HTTP_HOST");
            if (string.IsNullOrWhiteSpace(message.Application)) message.Application = message.Hostname;

            // Fix Url
            if (string.IsNullOrWhiteSpace(message.Url)) { message.Url = message.Data.GetValue("Url"); }
            if (string.IsNullOrWhiteSpace(message.Url))
            {
                if (message.ServerVariables.GetValue("HTTPS") == "off")
                {
                    message.Url = string.Format("http://{0}{1}", message.ServerVariables.GetValue("HTTP_HOST"), message.ServerVariables.GetValue("URL"));
                }
                else
                {
                    message.Url = string.Format("https://{0}{1}", message.ServerVariables.GetValue("HTTP_HOST"), message.ServerVariables.GetValue("URL"));
                }
            }

            // save message		
            using (LoggerContext db = new LoggerContext())
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
					
					db.LogMessages.Add(entry);
					db.SaveChanges();

					// created
					// Location: https://elmah.io/api/v2/messages?id=6707A1B0A79C8E85&logid=5082a1ce-c234-4c2e-92d4-5c5bd5a72854

					string url = string.Format("{0}api/v2/messages?id={1}&logid={2}", ElmahIoSettings.Url, entry.Id, entry.Log.LogId);

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
			using (LoggerContext db = new LoggerContext())
			{
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
			}

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
