using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface IPageManager
    {
		int Add(int siteId, int? parentId);

        List<PageObject> Roots(int siteId);        
        List<PageObject> Children(int pageId);
        List<PageObject> Children(int siteId, int? pageId);
        List<PageObject> Path(string pathId);

		int Copy(int siteId, int pageId);
		void Delete(int siteId, int pageId);
        //PageObject Load(int siteId, long sessionId, int? pageId, string pageName);
        PageObject Detail(int siteId, int pageId);
        PageObject Detail(int siteId, long sessionId, int pageId);
        PageObject Detail(int siteId, string pageUrl);		
        PageObject Detail(int siteId, long sessionId, string pageUrl);
		void SetSiteAncestorID(int siteId, int? pageId);
    	void Update(int siteId, PageObject page);
        void Update(int siteId, PageObject page, bool inherit);
        //void Update(int siteId, int pageID, int? parentId, string name, string Description, string URL, string query, int order, string theme, string template, string largeImage, string smallImage, bool hidden, string Target, bool ssl);

        void AddContent(long sessionId, int pageId, int contentId);
        void RemoveContent(long sessionId, int pageId, int contentId);

        List<PageObject> List(int siteId);
        List<PageObject> List(int siteId, int parentId);

        List<PageContentObject> ContentList(int pageId);
        List<PageContentObject> ContentList(string path);
    }
}
