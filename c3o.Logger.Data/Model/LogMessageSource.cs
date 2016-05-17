using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace c3o.Logger.Data
{
	public class LogMessageSource : INameId
	{
		public long Id { get; set; }
		public string Name { get; set; }
       // public List<Filter> Filters { get; set; }

        public LogMessageSource()
        {
          //  this.Filters = new List<Filter>();
        }

    }
}