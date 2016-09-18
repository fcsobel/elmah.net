using System;
using System.Collections.Generic;
using System.Linq;
using c3o.Logger.Data;

namespace c3o.Logger.Web
{
	public class SiteResponseModel
	{
		public string Name { get; set; }
		public ISiteRecord Site { get; set; }
		public string Notes { get; set; }

		public SiteResponseModel()
		{

		}

		public SiteResponseModel(ISiteInstance siteInstance) : base()
		{
			this.Name = siteInstance.SiteName;
			this.Site = siteInstance.Site;
		}
	}
}