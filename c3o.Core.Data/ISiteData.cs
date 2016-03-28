using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface ISiteData
	{
		NullableDataReader AdminMap(int siteID);
	   // NullableDataReader AdminTemplates(int siteId);
		void Update(int siteID, string siteName, string siteDesc, string keywords, string siteURL, string emailFrom, string emailCC, string emailBCC);
		void Copy(string SiteID, string NewSiteID);
		NullableDataReader List();
		NullableDataReader List(int siteID);
		NullableDataReader Routes(int siteId);
		NullableDataReader SecurityReport(string SiteID);
		void SessionCleanup();
		//NullableDataReader SiteMap(int siteID, long sessionID);
		NullableDataReader PermissionPageList(string PermissionName, string siteID);
		NullableDataReader CountByServer();
		NullableDataReader UserActivity(int siteID);
		NullableDataReader ActiveSessions(int siteId);
		NullableDataReader AllSessions(int siteId, bool activeOnly, bool userSessionsOnly, DateTime? specificDate);

	}
}
