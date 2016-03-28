using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IRoleData
	{
		NullableDataReader Detail(int roleId);
		NullableDataReader Detail(string name);

		NullableDataReader List();
		NullableDataReader List(int siteId, long sessionId);
		//NullableDataReader List(int siteId, long sessionId, string roleType, long itemId);
		//NullableDataReader GetObjectsInRole(int siteId, long sessionId, int iRoleID, string sType);

		//bool Verify(int siteId, long sessionId, string role);
		void Remove(int siteId, long itemId, int? roleId, string sType);
		void Assign(int siteId, long itemId, int roleId, string sType);

		void Delete(int id);
		int Add(string name, string description, string definition, string type);
		void Update(int id, string name, string description, string definition, string type);

		NullableDataReader AvailableRoles(string login);
		NullableDataReader UserRoles(string login);
		NullableDataReader UsersInRole(int siteId, int roleId);
		NullableDataReader UsersInRole(int siteId, string roleName);

		NullableDataReader UsersInRoles(int siteId, string roles);
		NullableDataReader UsersInRoles(int siteId, string roles, string groupPathId);


		//void AddRole(int siteId, string userName, string roleName);
	}
}
