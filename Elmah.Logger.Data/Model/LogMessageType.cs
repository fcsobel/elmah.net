using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmah.Net.Logger.Data
{
	public class LogMessageType : INameId
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Color { get; set; }

        public LogMessageType()
		{
			this.Icon = "cloud";
		}
	}
}