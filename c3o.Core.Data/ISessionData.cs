using System;
using  c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface ISessionData
    {
        int AddData(long iSessionID, string sName, string sData, bool isUserData);
        int RemoveData(long iSessionID, string sName);

        int AddObjectData(long objectId, string name, string data, string objectType);
        int RemoveObjectData(long objectId, string name, string objectType);

        //NullableDataReader Start(int iSiteID, long? iSessionID, string sSessionKey, string sServerName, string sReferringUrl, string sEntryPoint, string sIPAddress);
        long? Start(int siteId, Nullable<long> sessionId, string sessionKey, string ipAddress, string referringUrl, string currentUrl);
        void End(int iSiteID, long iSessionID);
        void Update(int iSiteID, long iSessionID, string iPageID, string url);

        // new
        bool CheckPermission(int siteId, long sessionId, string permission);
        int IncrementLockout(long Id);
        int Unlock(long Id);
        int Lock(long Id);
        int LogEvent(long sessionId, long objectId, string objectType, string text);
        long? Login(int siteId, long sessionId, string login);

        //new
        NullableDataReader GetData(long sessionId);
        string GetData(long sessionId, string name);
        

        NullableDataReader GetRoles(long sessionId);
                
        NullableDataReader ListRoles(int iSiteID, long iSessionID);

        NullableDataReader GetSession(string sessionKey);
        //NullableDataReader List(int iSiteID, long iSessionID);
        NullableDataReader GetSession(int siteId, long sessionId);        
        NullableDataReader GetSession(int siteId, string sessionKey);

        void AddRole(int site_id, long session_id, string role, string role_type);
        void AddRoles(int siteID, long sessionID, string roleNames, string roleType);
        void DeleteRole(int siteID, long sessionID, string roleName);
        void DeleteRoles(int siteID, long sessionID, string roleNames);

        string GetViewState(long iSessionID);
        void SetViewState(long iSessionID, string ViewState);

        void AddSiteMessage(int site_id, long session_id, string message);

        NullableDataReader FolderPermisions(long sessionId);
        NullableDataReader GroupFolderPermisions(long sessionId);
    }
}
