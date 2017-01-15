using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Elmah.Net.Logger.Data
{
    public class Filter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Distribution { get; set; }

        [NotMapped]
        public Query Query { get; set; }

        public string QueryData
        {
            get
			{
				return JsonConvert.SerializeObject(this.Query);
			}
            set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					this.Query = JsonConvert.DeserializeObject<Query>(value);
				}

			}
        }

        public Filter()
        {
            this.Query = new Query();
        }
    }
}