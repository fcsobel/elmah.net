using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c3o.Web.Site.Data
{
	public interface ICommonRepository
	{
		//AccountContentData AccountContentData { get; set; }
		//AccountData AccountData { get; set; }
		//AddressData AddressData { get; set; }
		//ChecklistData ChecklistData { get; set; }
		IContentManagementData ContentManagementData { get; set; }
		//DynamicData DynamicData { get; set; }
		IFileData FileData { get; set; }
		ILookupData LookupData { get; set; }
		//MailData MailData { get; set; }
		INavigationData NavigationData { get; set; }
		IPersonData PersonData { get; set; }
		//ReportsData ReportsData { get; set; }
		ISessionData SessionData { get; set; }
		ISessionArchiveData SessionArchiveData { get; set; }
		//SharedContentSql SharedContentSql { get; set; }
		//SharedTagSql SharedTagSql { get; set; }
		//SiteComponentData SiteComponentData { get; set; }
		//SiteContentData SiteContentData { get; set; }
		ISiteData SiteData { get; set; }
		IPageRouteData PageRouteData { get; set; }
		IPageData PageData { get; set; }
		//DetailUserData DetailUserData { get; set; }
		//DetailData DetailData { get; set; }
		IRoleData RoleData { get; set; }
		//StyleSheetData StyleSheetData { get; set; }
		//WorkFlowData WorkFlowData { get; set; }
		//IUserData UserData {get; set;}
		//IResourceData ResourceData { get; set; }
		IGroupData GroupData { get; set; }
		IContentData ContentData { get; set; }
		IFormData FormData { get; set; }
		IFormFileData FormFileData { get; set; }
		IFolderData FolderData { get; set; }
		IPersonContactData PersonContactData { get; set; }
        IPersonIdentityData PersonIdentityData { get; set; }		
	}
}
