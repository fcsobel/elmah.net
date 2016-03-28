using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace c3o.Web.Site.Data
{
	public class CommonRepository : ICommonRepository
	{
		public static ICommonRepository Current = Container.Current.Resolve<ICommonRepository>(); 

		//public AccountContentData AccountContentData { get; set; }
		//public AccountData AccountData { get; set; }
		//public AddressData AddressData { get; set; }
		//public ChecklistData ChecklistData { get; set; }
		public IContentManagementData ContentManagementData { get; set; }
		//public DynamicData DynamicData { get; set; }
		public IFileData FileData { get; set; }
		public ILookupData LookupData { get; set; }
		//public MailData MailData { get; set; }
		public INavigationData NavigationData { get; set; }
		public IPersonData PersonData { get; set; }
		//public ReportsData ReportsData { get; set; }
		public ISessionData SessionData { get; set; }
		public ISessionArchiveData SessionArchiveData { get; set; }
		//public SharedContentSql SharedContentSql { get; set; }
		//public SharedTagSql SharedTagSql { get; set; }
		//public SiteComponentData SiteComponentData { get; set; }
		//public SiteContentData SiteContentData { get; set; }
		public ISiteData SiteData { get; set; }
		public IPageRouteData PageRouteData { get; set; }
		public IPageData PageData { get; set; }
		//public DetailUserData DetailUserData { get; set; }
		//public DetailData DetailData { get; set; }
		public IRoleData RoleData { get; set; }
		//public StyleSheetData StyleSheetData { get; set; }
		//public WorkFlowData WorkFlowData { get; set; }
		//public IUserData UserData {get; set;}
		//public IResourceData ResourceData { get; set; }
		public IGroupData GroupData { get; set; }
		public IContentData ContentData { get; set; }

		public IFormData FormData { get; set; }
		public IFormFileData FormFileData { get; set; }

		public IFolderData FolderData { get; set; }
		public IPersonContactData PersonContactData { get; set; }
        public IPersonIdentityData PersonIdentityData { get; set; }
		
	}
}
