using System;
using System.Collections.Generic;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IStylesheetManager
	{
		void Add(int iSiteID, long iSessionID, string iPageID, string sClassName);
		void Delete(int iSiteID, long iSessionID, string iPageID, string sClassName);
		NullableDataReader List(int iSiteID, long iSessionID, string iPageID);
		NullableDataReader List(int iSiteID, long iSessionID, string iPageID, string sClassName);
		void Update(int iSiteID, long iSessionID, string iPageID, string sClassName, string sStyle);
	}
}
