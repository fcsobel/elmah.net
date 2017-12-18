using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Net.Logger.Data
{
	public class SearchQuery
	{
		protected LoggerContext db { get; set; }

		public SearchQuery(LoggerContext context)
		{
			this.db = context;
		}


		/// <summary>
		/// Return List of Log Messages based on query
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public IQueryable<LogMessage> Search(Query query)
		{
			//// convert to utc
			//if (query.Start.HasValue) query.Start.Value.ToUniversalTime();
			//if (query.End.HasValue) query.End.Value.ToUniversalTime();

			//// convert to span
			//if (!query.Start.HasValue && query.Span > 0)
			//{
			//	query.Start = DateTime.UtcNow.AddMinutes((int)query.Span * -1);
			//}

			IQueryable<LogMessage> messages = null;
			DateTime? start = null;
			DateTime? end = null;

			switch (query.Span)
			{
				case SearchSpan.None: // search by date
					if (query.Start.HasValue) start = query.Start.Value.ToUniversalTime();
					if (query.End.HasValue) end = query.End.Value.ToUniversalTime();
					if (start.HasValue && end > start)
					{
						messages = (IQueryable<LogMessage>)db.LogMessages.Where(x => x.DateTime >= start && x.DateTime <= end);
					}
					else
					{
						messages = (IQueryable<LogMessage>)db.LogMessages.Where(x => x.DateTime >= start);
					}
					break;
				case SearchSpan.All: // search all
					messages = (IQueryable<LogMessage>)db.LogMessages;
					break;
				default: // convert span to start date
					start = DateTime.UtcNow.AddMinutes((int)query.Span * -1);
					messages = (IQueryable<LogMessage>)db.LogMessages.Where(x => x.DateTime >= start);
					break;
			}

			//if (query.Start.HasValue)
			//{
			//	if (query.End.HasValue)
			//	{
			//		messages = (IQueryable<Elmah.Net.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= query.Start && x.DateTime <= query.End);
			//	}
			//	else
			//	{
			//		messages = (IQueryable<Elmah.Net.Logger.Data.LogMessage>)db.LogMessages.Where(x => x.DateTime >= query.Start);
			//	}
			//}
			//else
			//{
			//	messages = (IQueryable<Elmah.Net.Logger.Data.LogMessage>)db.LogMessages;
			//}

            // check for search text
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                messages = messages.Where(x => x.Title.Contains(query.Search) || x.Detail.Content.Contains(query.Search));
            }

            ////Log theLog = null;
            //if (!string.IsNullOrWhiteSpace(log))
            //{
            //	//var start = DateTime.Now.Date;
            //	//var allLogs = (IQueryable<Elmah.Net.Logger.Data.Log>)db.Logs.OrderBy(x => x.Name);
            //	var theLog = db.Logs.FirstOrDefault(x => x.Name == log);
            //	if (theLog != null)
            //	{
            //		messages = messages.Where(x => x.LogId == theLog.Id);
            //	}
            //}

            if (query.Logs != null && query.Logs.Any())
			{
				messages = messages.Where(x => query.Logs.Any(y => y == x.LogId));
			}

			//// application
			//if (!string.IsNullOrWhiteSpace(application))
			//{
			//	var obj = db.LogApplications.FirstOrDefault(x => x.Name == application);
			//	if (obj != null)
			//	{
			//		messages = messages.Where(x => x.ApplicationId == obj.Id);
			//		//logs = logs.Where(x => x.Applications.Any(a => a.Id == obj.Id));						
			//	}
			//}

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

			//if (query.Limit > 0)
			//{
			//	messages = messages.Take(query.Limit);
			//}

			// Includes and order by
			messages = messages
				.Include(x => x.Log)
				.Include(x => x.User)
				.Include(x => x.Source)
				.Include(x => x.MessageType)
				.Include(x => x.Application)
				.OrderByDescending(x => x.DateTime);

			return messages;
		}
	}
}