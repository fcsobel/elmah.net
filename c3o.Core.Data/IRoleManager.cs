using System;
using c3o.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace c3o.Web.Site.Data
{
    public interface IRoleManager
    {
        [OperationContract]
        List<RoleObject> List();

        [OperationContract(Name = "List1")]
        List<RoleObject> List(int siteId, long sessionId);

        //[OperationContract(Name = "List2")]
        //List<RoleObject> List(int siteId, long sessionId, long objectId, RoleType objectType);

        //[OperationContract]
        //List<IdObject> GetObjectsInRole(int siteId, long sessionId, int iRoleID, RoleType sType);

        [OperationContract]
        void Assign(int iSiteID, RoleType sType, long iItemID, int iRoleID);

		[OperationContract(Name = "Addign2")]
		void Assign(int iSiteID, RoleType sType, long iItemID, string roleName);


		//[OperationContract]
		void Remove(int iSiteID, RoleType sType, long iItemID, int iRoleID);

		[OperationContract(Name = "Remove2")]
		void Remove(int iSiteID, ObjectType sType, long iItemID, string roleName);
				
        [OperationContract]
        void UpdateRoles(int siteId, long objectId, RoleType objectType, List<RoleObject> roles);


        //[OperationContract]
        //bool Verify(int iSiteID, long iSessionID, string sRole);

        //[OperationContract]
        //List<PersonObject> GetUsersInRole(string roleName);

        [OperationContract]
        RoleObject Detail(int roleId);

		[OperationContract]
		RoleObject Detail(string roleName);
		
        [OperationContract]
        void Delete(int roleId);

        [OperationContract]
        RoleObject Update(RoleObject obj);


        [OperationContract]
        List<RoleObject> GetAvailableRoles(string userName);

        [OperationContract]
        List<RoleObject> GetRoles(string objectName, ObjectType objType);

        [OperationContract]
        List<PersonObject> GetUsersInRole(int siteId, string roleName);

        [OperationContract(Name = "GetUsersInRole1")]
        List<PersonObject> GetUsersInRole(int siteId, int roleId);

		[OperationContract]
		List<PersonObject> UsersInRoles(int siteId, string roleName);

		[OperationContract(Name = "GetUsersInRoles1")]
		List<PersonObject> UsersInRoles(int siteId, string roleName, string groupPathId);

        //[OperationContract]
        //void AddRoles(int siteId, string user, List<RoleObject> roles);
    }
}
