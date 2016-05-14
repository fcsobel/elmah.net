using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace c3o.Logger.Data
{
    public class Filter 
	{
		public long Id { get; set; }
		public string Name { get; set; }
        public string Distribution { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End  { get; set; }
        public SearchSpan Span { get; set; } 
        public int Limit { get; set; }

        public List<LogMessageType> FilterTypes { get; set; }
        public List<LogMessageSource> FilterSources { get; set; }

        public Filter()
		{
            this.FilterTypes = new List<LogMessageType>();
            this.FilterSources = new List<LogMessageSource>();
		}
	}

    public enum SearchSpan
    {
        [Description("Five Minutes")]
        FiveMinutes = 5,
        [Description("Fifteen Minutes")]
        FifteenMinutes = 15,
        [Description("One Hour")]
        OneHour = 60,
        [Description("Two Hours")]
        TwoHours = 120,
        [Description("Three Hours")]
        ThreeHours = 180,
        [Description("Four Hours")]
        FourHours = 240,
        [Description("Six Hours")]
        SixHours = 360,
        [Description("Twelve Hours")]
        TwelveHours = 1260,
        [Description("Twenty Four Hours")]
        TwentyFourHours = 1440,
        [Description("Three Days")]
        ThreeDays = 4320,
        [Description("One Week")]
        OneWeek = 10080,
        [Description("All")]
        All = 0
    }
}