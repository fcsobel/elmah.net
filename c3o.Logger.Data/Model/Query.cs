using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Newtonsoft.Json;
using Elmah.Io.Client;

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
        public List<Severity> Severities { get; set; }

        public Query()
        {
            this.Types = new List<long>();
            this.Sources = new List<long>();
            this.Users = new List<long>();
            this.Logs = new List<long>();
            this.Applications = new List<long>();
            this.Severities = new List<Severity>();
        }
    }
}