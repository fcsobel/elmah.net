using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IGroupData
	{
		void Assign(long personId, int groupId);
		void Delete(int id);
		NullableDataReader Detail(string name);
		NullableDataReader Detail(int id);
		NullableDataReader List();
		NullableDataReader List(int parentId);
		NullableDataReader Children();
		NullableDataReader Children(int parentId);
		NullableDataReader Path(string pathId);
		NullableDataReader PersonList(int groupId);
		void Remove(long personId);
		void Remove(long personId, int groupId);
		NullableDataReader RoleList(int siteId, long groupId);
		//int Update(long sessionId, int? id, string name, string type, string description, string status);
		NullableDataReader GetData(long id);
		NullableDataReader FolderPermissions(long groupId);
		void DeleteFolderPermissions(long groupId);
		int Add(long sessionId, string name, string type, string description, string status, int order, string pathId, int? parentId);
		void Update(long sessionId, int id, string name, string type, string description, string status, int order, string pathId, int? parentId);
	}
}
