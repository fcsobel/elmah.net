using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public enum EmailPurpose
	{
		Activation,
		PasswordChange
	}
    public interface IPersonData
    {
        NullableDataReader List(string personIDList);
        //NullableDataReader List(long? personID);
        //string Update(string PersonID, string LastName, string FirstName, string MiddleName);

        // NEW
        NullableDataReader List();
        NullableDataReader Detail(long personID);
        NullableDataReader Detail(string login);
        NullableDataReader LoadByEmail(string email);
        NullableDataReader AssignedRoles(int siteId, long personId);

        NullableDataReader EffectiveRoles(int siteId, long personId);
        NullableDataReader ActivityByUser(int personId);

        //NullableDataReader AssignedFolders(long personId);

        NullableDataReader AssignedGroups(long personId);
        NullableDataReader AssignedGroups(long personId, string type);
        NullableDataReader EffectiveGroups(long personId);

        NullableDataReader SessionList(int siteId, long personId);
        NullableDataReader GetData(long id);
		NullableDataReader Search(int siteId, string name, string firstName, string middleName, string lastName, string email, int? locationId, int? companyId, int? groupId, int? roleId, string lockoutCode);

        int Update(long personId, string lastName, string firstName, string middleName, string authentication, string login, string password, string email, DateTime? loginDate, DateTime? passwordDate, int passwordCount, string LockoutCode, bool Reset_Password, int Reset_Count, int? GroupId, int passwordFormat, string PasswordSalt);
        long Add(string lastName, string firstName, string middleName, string authentication, string login, string password, string email, DateTime? loginDate, DateTime? passwordDate, int passwordCount, string LockoutCode, bool Reset_Password, int Reset_Count, int? GroupId, int passwordFormat,  string PasswordSalt);

        void ChangePassword(long sessionId, long personId, string oldpassword, string password);
        void Delete(long sessionId, long personId);

        NullableDataReader FolderPermisions(long personId);
        NullableDataReader GroupFolderPermisions(long personId);
    }
}
