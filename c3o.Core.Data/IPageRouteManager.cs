using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface IPageRouteManager
    {
        void Add(int pageId, PageRouteObject obj);
        void Add(int pageId, List<PageRouteObject> list);
        void Delete(int pageId, int routeOrder);
        void Delete(int pageId);
        List<PageRouteObject> List(int pageId);
        void Update(int pageId, List<PageRouteObject> list);
        void Update(int pageId, int routeOrder, string routeUrl, string routePage);
    }
}
