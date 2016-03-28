using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using c3o.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;


namespace c3o.Web.Site.Data
{
	public class CommonTransport : ICommonTransport
	{
		public static ICommonTransport Current = Container.Current.Resolve<ICommonTransport>(); 
		public INavigationManager NavigationManager { get; set; }
		public ISiteManager SiteManager { get; set; }
		//public IAccountContentManager AccountContentManager { get; set; }
		//public IAccountManager AccountManager { get; set; }
		public IDiskManager DiskManager { get; set; }
		public IFileManager FileManager { get; set; }
		public IFormManager FormManager { get; set; }
		public IFolderManager FolderManager { get; set; }
		public ILookupDataManager LookupDataManager { get; set; }
		public ISessionManager SessionManager { get; set; }
		public IContentManager ContentManager { get; set; }
		public IRoleManager RoleManager { get; set; }
		public ISharedTagManager TagManager { get; set; }
		public IPageRouteManager PageRouteManager { get; set; }
		public IAddressManager AddressManager { get; set; }
		public IPersonManager PersonManager { get; set; }
		public IPageManager PageManager { get; set; }
		//public IUserManager UserManager { get; set; }
		public IStylesheetManager StylesheetManager { get; set; }
		public IDetailManager DetailManager { get; set; }
		public IDetailUserManager DetailUserManager { get; set; }
		public IPageHeirarchyManager PageHeirarchyManager { get; set; }
		public IResourceDataManager ResourceDataManager { get; set; }
		public IGroupManager GroupManager { get; set; }
		public ITransferManager TransferManager { get; set; }
		//public IShippingMManager ShippingMManager { get; set; }		
	}
}
