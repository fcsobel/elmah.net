using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmah.Net.Logger.Data
{
	public class LogMessageSource : INameId
	{
		public long Id { get; set; }
		public string Name { get; set; }
    }
}