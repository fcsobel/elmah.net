using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace c3o.Logger.Data
{
	public class Filter 
	{
		public long Id { get; set; }
		public string Name { get; set; }
        public string Distribution { get; set; }

        public List<LogMessageType> FilterTypes { get; set; }
        public List<LogMessageSource> FilterSources { get; set; }

        public Filter()
		{
            this.FilterTypes = new List<LogMessageType>();
            this.FilterSources = new List<LogMessageSource>();
		}
	}
}