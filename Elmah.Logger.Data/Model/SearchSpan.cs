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
        [Description("5 Minutes")]
        FiveMinutes = 5,
        [Description("15 Minutes")]
        FifteenMinutes = 15,
        [Description("1 Hour")]
        OneHour = 60,
        [Description("2 Hours")]
        TwoHours = 60 * 2,
        [Description("4 Hours")]
        FourHours = 60 * 4,
        [Description("6 Hours")]
        SixHours = 60 * 6,
        [Description("12 Hours")]
        TwelveHours = 60 * 12,
        [Description("24 Hours")]
        TwentyFourHours = 60 * 24,
        [Description("3 Days")]
        ThreeDays = 60 * 24 * 3,
        [Description("One Week")]
        OneWeek = 60 * 24 * 7,
        [Description("30 Days")]
        ThirtyDays = 60 * 24 * 30,
        [Description("60 Days")]
        SixtyDays = 60 * 24 * 60,
        [Description("90 Days")]
        NinetyDays = 60 * 24 * 90,
        [Description("One Year")]
        OneYear = 60 * 24 * 365,
        [Description("All")]
        All = 0
    }
}