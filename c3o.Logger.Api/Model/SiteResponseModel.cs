using System;
using System.Collections.Generic;
using System.Linq;
using c3o.Core;
using c3o.Logger.Data;

namespace c3o.Logger.Web
{    
    public class SiteResponseModel
	{
        public string Name { get; set; }
        public Data.SiteRecord Site { get; set; }

        public SiteResponseModel()
        {

        }

        public SiteResponseModel(SiteContext context) : base()
        {
            this.Name = context.SiteName;
            this.Site = context.Site;

        }
	}
}