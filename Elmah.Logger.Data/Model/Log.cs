using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmah.Net.Logger.Data
{
	public class Log : INameId
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public Guid LogId { get; set; }
		public List<LogApplication> Applications { get; set; }
		public List<LogMessage> Messages { get; set; }

		public Log()
		{
			this.Applications = new List<LogApplication>();
			this.Messages = new List<LogMessage>();
		}
	}
}