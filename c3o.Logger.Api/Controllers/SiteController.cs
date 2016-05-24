using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Elmah.Io.Client;
using c3o.Logger.Data;
using c3o.Core;

namespace c3o.Logger.Web
{
    [RoutePrefix("api.logger/sites")]
    public class SiteController : ApiController
    {
        protected SiteContext SiteContext { get; set; }

        public SiteController(SiteContext siteContext) 
        {
            this.SiteContext = siteContext;
        }

        [HttpGet]
        [Route("")]
        public SiteResponseModel Detail()
        {
            return new SiteResponseModel(this.SiteContext);
        }

        [HttpDelete]
        [Route("")]
        public SiteResponseModel Delete(long id)
        {
            return new SiteResponseModel(this.SiteContext);
        }


        [HttpPost]
        [Route("")]
        public SiteResponseModel Create(SiteRecord model)
        {
            return new SiteResponseModel(this.SiteContext);
        }
    }
}