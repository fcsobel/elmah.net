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
	public interface ICommonTransport 
	{
		INavigationManager NavigationManager {get; }
		ISiteManager SiteManager { get;  }
		//IAccountContentManager AccountContentManager { get;  }
		//IAccountManager AccountManager { get;  }
		IDiskManager DiskManager { get; }
		IFileManager FileManager { get;  }
		IFormManager FormManager { get;  }
		IFolderManager FolderManager { get; }
		ILookupDataManager LookupDataManager { get;  }
		ISessionManager SessionManager { get;  }
		IContentManager ContentManager { get; }
		IRoleManager RoleManager {get; }    
		ISharedTagManager TagManager {get; }   
		IPageRouteManager PageRouteManager {get; }
		IAddressManager AddressManager {get; }
		IPersonManager PersonManager  {get; }
		IPageManager PageManager {get; }
		//IUserManager UserManager { get;  }
		IStylesheetManager StylesheetManager { get;  }
		IDetailManager DetailManager { get;  }
		IDetailUserManager DetailUserManager { get;  }
		IPageHeirarchyManager PageHeirarchyManager { get;  }
		IResourceDataManager ResourceDataManager { get; }
		IGroupManager GroupManager { get; }
		ITransferManager TransferManager { get; }
	}
}
