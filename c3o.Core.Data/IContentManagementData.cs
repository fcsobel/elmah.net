using System;
using System.Data;

namespace c3o.Web.Site.Data
{
	public interface IContentManagementData
	{
        //void AddComponent(string component_id, string DestinationDB);
        //void AddComponentSecurity(string component_id, string role_id, string SourceDB, string DestinationDB);
        //void AddComponentSite(string component_id, string site_id, string DestinationDB);
		void AddPage(string page_id, string DestinationDB);
		//void AddPageDetail(string detail_id, string SourceDB, string DestinationDB);
		//void AddPageDetailSecurity(string detail_id, string role_id, string SourceDB, string DestinationDB);
        //void AddPageQuestion(string question_id, string SourceDB, string DestinationDB);
		
		void AddPageSecurity(string page_id, string role_id, string SourceDB, string DestinationDB);
        //void AddPageStyleSheet(string page_id, string class_name, string DestinationDB);
		void AddRole(string role_id, string SourceDB, string DestinationDB);
		void AddSite(string site_id, string DestinationDB);
        void AddGroup(string Id, string DestinationDB);
        void AddGroupSecurity(string siteId, string groupId, string roleId, string SourceDB, string DestinationDB);

		DataTable Compare(System.Data.DataTable dt1, System.Data.DataTable dt2, bool boolShowAll);
        DataTable Compare(string db1, string sql1, string[] keys1, string[] skip1, string db2, string sql2, string[] keys2, string[] skip2, bool boolShowAll);		
		DataTable ComponentList();
		string Copy(string sqlWhere, string table, string key, string SourceDB, string DestinationDB);
		void Copy(string content_id, string source, string destination);
		string Copy(string sqlWhere, string table, string[] keys, string[] skip, string SourceDB, string DestinationDB);
		string Copy(string sqlWhere, string table, string key, string skip, string SourceDB, string DestinationDB);
        //void DeleteComponentSite(string component_id, string site_id, string DestinationDB);
		//void DeletePageDetail(string detail_id, string DestinationDB);
        //void DeletePageQuestion(string question_id, string DestinationDB);
		
        //void DeletePageStyleSheet(string SiteID, string page_id, string class_name, string DestinationDB);
		void DeleteSecurity(string object_type, string site_id, string page_id, string role_id, string DestinationDB);
		string Preview(string content_id, string source);

        void AddPageRoute(string page_id, int rouute_order, string route_url, string DestinationDB);
        
        //void DeletePageRoute(string page_id, string route_url, string DestinationDB);
        void DeletePageRoute(string pageId, string routeUrl, string destination);
        void DeletePageRoute(string pageId, int routeOrder, string destination);

        void AddData(string objectType, string objectId, string name, string DestinationDB);
        void DeleteData(string objectType, string objectId, string name, string DestinationDB);

       

	}
}
