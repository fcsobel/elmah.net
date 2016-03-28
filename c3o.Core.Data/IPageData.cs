using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IPageData
    {
        int Add(int siteId, int? parentId);
        NullableDataReader Children(int pageId);
        NullableDataReader Roots(int siteId);
        int Copy(int siteId, int pageId);
        void Delete(int siteId, int pageId);

        NullableDataReader Detail(int siteId, int pageId);
        NullableDataReader Detail(int siteId, long sessionId, int pageId);
        NullableDataReader Detail(int siteId, string pageUrl);
        NullableDataReader Detail(int siteId, long sessionId, string pageUrl);

        //NullableDataReader Load(int siteId, long SessionID, int? pageId, string name);
        NullableDataReader List(int siteId);
        NullableDataReader List(int siteId, int parentId);

        NullableDataReader RoleList(long pageId);
        //NullableDataReader GetData(long id);
        //NullableDataReader Path(string pathId);
        int Update(int siteId, int pageId, int? parentID, string name, string description, string url, string query, int order, string Theme, string Template, string largeImage, string smallImage, bool hidden, string target, bool ssl);

        //string Parameters(int pageId);
        void SetSiteAncestor(int siteId, int? pageId);
        NullableDataReader Path(string pathId);

        void AddContent(long sessionId, int pageId, int contentId);
        NullableDataReader ContentList(int pageId);
        NullableDataReader ContentList(string path);

        //void UpdateParameters(int pageId, string xml);
        //NullableDataReader ComponentList(int siteId, long sessionId, int pageId, int width);
        NullableDataReader Permisions(int folderId);
        void RemovePermissions(int pageId);
        void RemovePermission(int pageId, int roleId);
        void AssignPermission(int pageId, int roleId, bool read, bool write, bool folders, bool delete, bool settings, bool inherit);
    }
}
