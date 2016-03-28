using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IDetailManager
	{
		void Add(int siteID, string pageID, string location);
		void Add(int siteID, string pageID, string location, string componentID, string detailID);
		void Delete(int iSiteID, string iPageID, object iDetailID);
		NullableDataReader List(int iSiteID, string iPageID, string sLocation, object iDetailID);
		void Move(string DetailID, string Location, int Order);
		string Parameters(int iDetailID);
		void Update(int siteID, string pageID, string detailID, string componentID);
		void Update(int iSiteID, string iPageID, string iDetailID, string iComponentID, string sLocation, string sTitle, string iBorder, string iOrder, string iPadding, string iSpacing, string sAlignment, string iWidth, string iHeight, string CssClass, string BorderStyle);
		void UpdateParameters(int iDetailID, string xml);
	}
}

namespace c3o.Web.Site.Data
{
	public interface IDetailUserManager
	{
		void Add(int iSiteID, long iSessionID, string iPageID, object componentID, string location);
		void Close(int iSiteID, long iSessionID, string iPageID, object iDetailID);
		void Delete(int iSiteID, long iSessionID, string iPageID, object iDetailID);
		void MoveDown(int iSiteID, long iSessionID, string iPageID, object iDetailID);
		void MoveUp(int iSiteID, long iSessionID, string iPageID, object iDetailID);
		void Open(int iSiteID, long iSessionID, string iPageID, object iDetailID);
		void Toggle(int iSiteID, long iSessionID, string iPageID, object iDetailID);
	}
}