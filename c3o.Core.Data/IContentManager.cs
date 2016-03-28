using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Data;

namespace c3o.Web.Site.Data
{
	public interface IContentManager
	{
		void Add(long sessionId, string title, string text, int? owner, ContentStatus status, string tags);
		void Delete(long sessionId, int contentId);
		void Delete(long sessionId, ContentObject content);
        List<ContentObject> Search(long sessionId, int? contentId, string contentName, int? accountId, string accountName, string tags, int index, int count);
        //void Update(long sessionId, ContentObject content, string tags);
        ContentObject Update(long sessionId, ContentObject content);
        ContentObject Update(long sessionId, ContentObject content, string tags);
        ContentObject Detail(int contentId);
        ContentObject Detail(int siteId, long sessionId, int contentId);
        ContentObject Detail(string title);
        List<CommentObject> Comments(int contentId);
		void Comment(long sessionId, CommentObject comment);

        void AttachFile(long sessionId, int contentId, int fileId);

        List<ContentObject> List(int siteId, long sessionId, string list);
        List<ContentObject> List(int siteId, long sessionId, List<int> list);


        [OperationContract]
        void AddPage(string Id, string destination);
        [OperationContract]
        void AddGroup(string Id, string destination);
        [OperationContract]
        void AddGroupSecurity(string siteId, string groupId, string roleId, string SourceDB, string DestinationDB);
        //[OperationContract]
        //void AddLocation(string Id, string destination);
        [OperationContract]
        void AddPageRoute(string pageId, int routeOrder, string routeUrl, string destination);
        [OperationContract]
        void AddPageSecurity(string pageId, string roleId, string source, string destination);
        [OperationContract]
        void AddRole(string roleId, string source, string destination);
        [OperationContract]
        void AddSite(string siteId, string destination);

        [OperationContract]
        void DeletePageRoute(string pageId, string routeUrl, string destination);

        [OperationContract]
        void DeletePageRoute(string pageId, int routeOrder, string destination);
        
        [OperationContract]
        void DeleteSecurity(string object_type, string siteId, string pageId, string roleId, string destination);
        [OperationContract]
        void AddData(string objectType, string objectId, string name, string DestinationDB);
        [OperationContract]
        void DeleteData(string objectType, string objectId, string name, string DestinationDB);
        [OperationContract]
        string Copy(string sqlWhere, string table, string key, string source, string destination);
        [OperationContract(Name = "Copy1")]
        string Copy(string sqlWhere, string table, string[] keys, string[] skip, string source, string destination);
        [OperationContract(Name = "Copy2")]
        string Copy(string sqlWhere, string table, string key, string skip, string source, string destination);
        [OperationContract]
        DataTable Compare(string db1, string sql1, string[] keys1, string[] skip1, string db2, string sql2, string[] keys2, string[] skip2, bool boolShowAll);


	}
}
