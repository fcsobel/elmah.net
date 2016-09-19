using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace c3o.Logger.Data
{
	public class LogApplication : INameId
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long LogId { get; set; }
		public Log Log { get; set; }
		public List<LogMessage> Messages { get; set; }
		public LogApplication()
		{
			this.Messages = new List<LogMessage>();
		}
	}
}