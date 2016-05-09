using DbUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace c3o.Logger.Data
{
    public class SiteInstance
    {
        public List<SiteRecord> Sites = new List<SiteRecord>()
        {
            new SiteRecord("www", "www"),
            new SiteRecord("acme", "acme"),
        };
    }
}