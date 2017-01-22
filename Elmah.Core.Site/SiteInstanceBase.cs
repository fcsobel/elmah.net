using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Elmah.Net.Logger.Data
{
	public class SiteInstanceBase : ISiteInstance
	{
		public ISiteRecord Site { get; set; }
		public virtual string Name { get { return "Logger"; } }
		public virtual string CreateSite(ISiteRecord site)
		{
			throw new NotImplementedException();
		}

		public SiteInstanceBase()
		{
			this.Site = new SiteRecordBase();
		}
	}
}