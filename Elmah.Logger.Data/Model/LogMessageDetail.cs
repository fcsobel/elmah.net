using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmah.Net.Logger.Data
{
	public class LogMessageDetail
	{
		public long Id { get; set; }
		public string Content { get; set; }
		public string ContentKey { get; set; }

		public new string ToString() { return this.Content; }

		public LogMessageDetail()
		{
				
		}
	}
}