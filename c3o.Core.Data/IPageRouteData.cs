using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IPageRouteData
    {
		void Delete(int pageId);
		void Delete(int pageId, int order);
        NullableDataReader List(int pageId);
		void Add(int pageId, int order, string routeUrl, string routePage);
        void Update(int pageId, int order, string routeUrl, string routePage);
    }
}
