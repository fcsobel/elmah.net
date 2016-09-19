using c3o.Core;
using Elmah.Io.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace c3o.Logger.Data
{
    public class Query
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public SearchSpan Span { get; set; }
        public int Limit { get; set; }
        public List<long> Logs { get; set; }
        public List<long> Applications { get; set; }
        public List<long> Types { get; set; }
        public List<long> Sources { get; set; }
        public List<long> Users { get; set; }
        public List<LogSeverity> Severities { get; set; }

        public void Hydeate(Query value)
        {
            this.Start = value.Start;
            this.End = value.Start;
            this.Span = value.Span;
            this.Limit = value.Limit;
            this.Types = value.Types;
            this.Sources = value.Sources;
            this.Applications = value.Applications;
            this.Logs = value.Logs;
            this.Users = value.Users;
            this.Severities = value.Severities;
        }

        public Query()
        {
            this.Limit = 100;
            this.Span = SearchSpan.TwentyFourHours;
            this.Logs = new List<long>();
            this.Applications = new List<long>();
            this.Types = new List<long>();
            this.Sources = new List<long>();
            this.Users = new List<long>();
            this.Severities = new List<LogSeverity>();
        }
    }
}