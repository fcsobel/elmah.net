using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Newtonsoft.Json;

namespace c3o.Logger.Data
{
    public class Filter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Distribution { get; set; }

        [NotMapped]
        public Query Query { get; set; }

        public string QueryData
        {
            get { return JsonConvert.SerializeObject(this.Query); }
            set { this.Query = JsonConvert.DeserializeObject<Query>(value); }

        }
    }
}