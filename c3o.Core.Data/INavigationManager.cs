using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface INavigationManager
    {
        List<PageObject> Children(int siteId, long sessionId);
		List<PageObject> Children(int siteId, long sessionId, int pageId);

		PageObject Page(int siteId, long sessionId, int pageId);
		PageObject Parent(int iPageID);
		PageObject Parent(int siteId, long sessionId, int pageId);
        List<PageObject> SiteMap(int siteID);
        List<PageObject> SiteMap(int siteID, long sessionID);
        
        List<PageContentObject> PopularContent(int siteId, long sessionId);
        List<PageContentObject> NewContent(int siteId, long sessionId);
        List<PageContentObject> FavoriteContent(int siteId, long sessionId, int limit);
        List<PageContentObject> FavoriteContent(int siteId, long sessionId, int limit, string listName);

        //List<PageObject> ChildMenu(int siteId, long sessionId);
        //List<PageObject> ChildMenu(int siteId, long sessionId, int pageId);
		//List<PageObject> Impersonate(int siteId, long sessionId, int pageId);
        //List<PageObject> Next(int siteId, long sessionId, int pageId);
		//List<PageObject> Previous(int siteId, long sessionId, int pageId);
        //List<PageObject> RootMenu(int siteId, long sessionId);
        //List<PageObject> Siblings(int siteId, long sessionId, int pageId);
        //List<PageObject> SubMenu(int siteId, long sessionId, int pageId);
        //List<PageObject> Top10(int siteId, long sessionId);
        //List<PageObject> TopMenu(int siteId, long sessionId);
        //List<PageObject> Trail(int siteId, long sessionId, int pageId);
    }
}
