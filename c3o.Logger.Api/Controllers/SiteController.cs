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
	[RoutePrefix("api.site")]
	public class SiteController : ApiController
	{
		protected ISiteInstance SiteInstance { get; set; }
		//private SiteContext db { get; set; }
		//private SiteList SiteList { get; set; }

		public SiteController(ISiteInstance siteInstance)  // SiteContext siteContext , SiteList siteList
		{
			// this.db = siteContext;
			this.SiteInstance = siteInstance;
			//  this.SiteList = siteList;

		}

		/// <summary>
		/// Get Site
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("sites")]
		public SiteResponseModel Detail()
		{
			return new SiteResponseModel(this.SiteInstance);
		}

		/// <summary>
		/// Create Site
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("sites")]
		public SiteResponseModel Create(SiteResponseModel model)
		{
			var message = this.SiteInstance.CreateSite(null);

			SiteResponseModel obj = new SiteResponseModel(this.SiteInstance);
			obj.Notes = message;
			return obj;
		}

		/// <summary>
		/// Delete Site
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("sites")]
		public SiteResponseModel Delete(long id)
		{
			return new SiteResponseModel(this.SiteInstance);
		}
	}
}