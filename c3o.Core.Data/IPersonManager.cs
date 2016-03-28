using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace c3o.Web.Site.Data
{
    public interface IPersonManager
    {
		//.net provider stuff
		[OperationContract]
		void AssignRoles(int siteId, string username, List<RoleObject> roles);
		[OperationContract]
		void RemoveRoles(int siteId, string username, List<RoleObject> roles);

       // List<PersonObject> List(string personIDList);
        //List<PersonObject> List(long? personID);
       // string Update(string PersonID, string LastName, string FirstName, string MiddleName);

        [OperationContract]
        PersonObject Update(int siteId, long sessionId, PersonObject obj);

        [OperationContract]
        void ChangePassword(long sessionId, long personId, string oldpassword, string password);

        [OperationContract]
        PersonObject Detail(int siteId, long personID);

        [OperationContract(Name = "Detail1")]
        PersonObject Detail(int siteId, string login);

        [OperationContract]
        PersonObject DetailByEmail(int siteId, string email);

        [OperationContract]
        List<PersonObject> List();

        [OperationContract(Name = "List1")]
        List<PersonObject> List(string personIDList);

        [OperationContract]
		List<PersonObject> Search(int siteId, string name, string firstName, string middleName, string lastName, string email, int? locationId, int? companyId, int? groupId, int? roleId, AccountStatus? lockoutReason);

        //[OperationContract(Name = "PagedSearch")]
        //PersonSearchResults Search(int siteId, string firstName, string middleName, string lastName, int? locationId, int? companyId, int? groupId, int? roleId, LockoutReasonEnum? lockoutReason, int pageIndex, int pageSize);

        //[OperationContract]
        //List<PersonObject> Search(int siteId, string login, string email, string firstName, string middleName, string lastName, int? locationId, List<int> companies, List<int> groups, List<int> roles, LockoutReasonEnum? lockoutReason, AuthenticationEnum? authenticaion, DateTime? loginDateStart, DateTime? loginDateEnd);

        //[OperationContract(Name = "PagedSearch")]
        //PersonSearchResults Search(int siteId, string login, string email, string firstName, string middleName, string lastName, int? locationId, List<int> companies, List<int> groups, List<int> roles, LockoutReasonEnum? lockoutReason, AuthenticationEnum? authenticaion, DateTime? loginDateStart, DateTime? loginDateEnd, int pageIndex, int pageSize);


        [OperationContract]
        List<SessionObject> SessionList(int siteId, long personId);

        [OperationContract]
        List<NameValueObject> GetData(long personId);
        [OperationContract]
        void AssignRole(int siteId, long personId, int roleId);

        [OperationContract(Name = "AssignRoleByName")]
        void AssignRole(int siteId, long personId, string roleName);

        [OperationContract]
        void RemoveRole(int siteId, long personId, int? roleId);

        [OperationContract]
        void UpdateRoles(int siteId, long personId, List<RoleObject> roles);

        [OperationContract]
        List<RoleObject> AssignedRoles(int siteId, long personId);

        [OperationContract]
        List<RoleObject> EffectiveRoles(int siteId, long personId);

        [OperationContract]
        void AssignGroup(long personId, int groupId);

        [OperationContract]
        void RemoveGroup(long personId);
        
        [OperationContract]
        void RemoveGroup(long personId, int groupId);

        [OperationContract]
        void UpdateGroups(long personId, List<GroupObject> groups);

        [OperationContract]
        List<GroupObject> AssignedGroups(long personId);

        [OperationContract(Name = "AssignedGroups1")]
        List<GroupObject> AssignedGroups(long personId, GroupType type);

        [OperationContract]
        List<GroupObject> EffectiveGroups(long personId);

        //[OperationContract]
        //void AssignLocation(long personId, int locationId);

        //[OperationContract]
        //void RemoveLocation(long personId, int? locationId);
        //[OperationContract]
        //void UpdateLocations(long personId, List<LocationObject> locations);
        
        //[OperationContract]
        //List<LocationObject> AssignedLocations(long personId);

        //[OperationContract]
        //List<LocationObject> EffectiveLocations(long personId);

        [OperationContract]
        List<LogObject> ActivityByUser(int personId);

        [OperationContract]
        void Delete(long sessionId, long personId);

        [OperationContract]
        List<FolderObject> FolderPermissions(long personId);

        //[OperationContract]
        //List<FolderPermissionObject> AssignedFolders(long personId);

		void EmailNotify(PersonObject Person, EmailPurpose purpose, string tempPassword, string template=null);

        List<PersonLoginProvider> Providers(long personId);
        void AddProvider(PersonLoginProvider provider);
        PersonObject Detail(string provider, string providerKey);
        void DeleteProvider(PersonLoginProvider provider);

    }
}
