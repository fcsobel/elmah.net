using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace c3o.Logger.Data
{
	public class SiteInstanceBase : ISiteInstance
	{
		SiteRecordBase _site { get; set; }

		public ISiteRecord Site { get { return _site; } }

		public string SiteName { get { return "Logger"; } }

		public string CreateSite(ISiteRecord site)
		{
			throw new NotImplementedException();
		}

		public SiteInstanceBase()
		{
			this._site = new SiteRecordBase();
		}
	}
}