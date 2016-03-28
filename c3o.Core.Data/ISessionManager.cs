using System;
using System.Collections.Generic;
using System.ServiceModel;
using c3o.Common;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface ISessionManager
	{
		int AddData(long sessionId, string name, string data);
		int AddData(long sessionId, string name, string data, bool isUserData);
		void RemoveData(long sessionId, string name);

		int AddItem(long sessionId, string name, string csv, string data, bool userData);
		int RemoveItem(long sessionId, string name, string csv, string data, bool userData);
				
		//SessionObject Start(SessionObject session);
		 SessionObject Start(int siteId, Nullable<long> sessionId, string sessionKey, string ipAddress, string referringUrl, string currentUrl);
		void End(SessionObject session);

		string GetViewState(SessionObject session);
		//NullableDataReader List(int iSiteID, long iSessionID);
		NullableDataReader ListRoles(int iSiteID, long iSessionID);
		
		void SetViewState(SessionObject session, string ViewsStateSting);
		void Update(SessionObject session, string page_id, string url);
		
		[OperationContract]
		List<RoleObject> GetRoles(long sessionId);

		void AddSiteMessage(SessionObject session, string message);


		[OperationContract]
		SessionObject GetSession(int siteId, long sessionId);

		[OperationContract(Name = "GetSession1")]
		SessionObject GetSession(string sessionKey);

		[OperationContract(Name = "GetSession2")]
		SessionObject GetSession(int siteId, string sessionKey);

		[OperationContract]
		LoginReturn Login(SessionObject session, string login, string password);

		[OperationContract]
		LoginReturn Login(SessionObject session, PersonObject person);
	 
		[OperationContract]
		int IncrementLockout(long Id);

		[OperationContract]
		int Unlock(long Id);

		[OperationContract]
		int Lock(long Id);

		[OperationContract]
		PersonObject LoadfromAD(PersonObject pobj);

		[OperationContract]
		bool IsUserLockedAD(PersonObject pobj);

		string ResetPassword(int siteId, long sessionId, string email);
	}
}
