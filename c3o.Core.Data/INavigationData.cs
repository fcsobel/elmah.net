using System;
using c3o.Data;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface INavigationData
    {
        NullableDataReader Children(int siteId, long sessionId);
        NullableDataReader Children(int siteId, long sessionId, int pageId);
        NullableDataReader Page(int siteId, long sessionId, int pageId);
        //NullableDataReader Page(int siteId, long sessionId, int? pageId);
        NullableDataReader Parent(int pageId);
        NullableDataReader Parent(int siteId, long sessionId, int pageId);
        NullableDataReader List(int siteId, long sessionId);
        //NullableDataReader List(int siteId, long sessionId, List<int> pages);
        //NullableDataReader List(int siteId, long sessionId, string pages);
        NullableDataReader FavoriteContent(int siteId, long sessionId, string pages);
        NullableDataReader FavoriteContent(int siteId, long sessionId, string pages, int limit);        
        NullableDataReader NewContent(int siteId, long sessionId);
        NullableDataReader PopularContent(int siteId, long sessionId);

        //NullableDataReader Children(int siteId, long sessionId);
        //NullableDataReader Children(int siteId, long sessionId, int pageId);


        //NullableDataReader ChildMenu(int siteId, long sessionId);
        //NullableDataReader ChildMenu(int SiteID, long SessionID, int PageID);
        
        //NullableDataReader Parent(int pageId);
        //NullableDataReader RootMenu(int siteId, long sessionId);
        //NullableDataReader Siblings(int siteId, long sessionId, int pageId);
        //NullableDataReader SubMenu(int siteId, long sessionId, int pageId);
        //NullableDataReader Top10(int SiteID, long SessionID);
        //NullableDataReader TopMenu(int siteId, long sessionId);
        //NullableDataReader Trail(int siteId, long sessionId, int pageId);
        //NullableDataReader List(int siteId, long sessionId);
    }
}
