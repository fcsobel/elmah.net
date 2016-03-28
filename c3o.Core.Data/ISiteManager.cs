using System;
using c3o.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.ServiceModel;

namespace c3o.Web.Site.Data
{
    public interface ISiteManager
    {
		List<PageObject> AdminMap(int siteId);
        //NullableDataReader AdminTemplates(int siteId);
        void Update(int siteID, string siteName, string siteDesc, string keywords, string siteURL, string emailFrom, string emailCC, string emailBCC);
        void Copy(string SiteID, string NewSiteID);
        SiteObject List(int siteID);
        List<SiteObject> List();
        List<PageRouteObject> Routes(int siteId);
        NullableDataReader SecurityReport(string SiteID);
        void SessionCleanup();
        //List<PageObject> SiteMap(int siteID, long sessionID);
        NullableDataReader CountByServer();
        List<PersonReportObject> UserActivity(int siteId);
        List<SessionObject> ActiveSessions(int siteId);
		void QueueMessage(MailMessage mail);

		//[OperationContract]
		List<SessionObject> AllSessions(int siteId, bool activeOnly, bool userSessionsOnly, DateTime? specificDate);
    }
}
