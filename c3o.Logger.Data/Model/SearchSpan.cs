using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Newtonsoft.Json;

namespace c3o.Logger.Data
{
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