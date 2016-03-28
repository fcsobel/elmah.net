using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
	public interface IPageHeirarchyManager
	{
        //List<IHeirarchyItem> ChildMenu(int siteId, long sessionId);
        //List<IHeirarchyItem> ChildMenu(int siteId, long sessionId, int pageId);
        //List<IHeirarchyItem> Children(int siteId, long sessionId, int pageId);
        //List<IHeirarchyItem> Next(int siteId, long sessionId, int pageId);
		PageObject Page(int siteId, long sessionId, int pageId);
		PageObject Parent(int iPageID);
        //List<IHeirarchyItem> Previous(int siteId, long sessionId, int pageId);
        //List<IHeirarchyItem> RootMenu(int siteId, long sessionId);
        //List<IHeirarchyItem> Siblings(int siteId, long sessionId, int pageId);
        //List<IHeirarchyItem> SubMenu(int siteId, long sessionId, int pageId);
        //List<IHeirarchyItem> Top10(int siteId, int sessionId);
        //List<IHeirarchyItem> TopMenu(int siteId, long sessionId);
        //List<IHeirarchyItem> Trail(int siteId, long sessionId, int pageId);
	}
}
