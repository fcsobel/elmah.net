//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;

//namespace c3o.Web.Site.Data
//{
//	[SerializableAttribute()]
//	public enum AccountStatus
//	{
//		Active,		// Active Account
//		Inactive,	// All Inactive Accounts
//		[Description("Inactive - New awaiting activation")]
//		New,		// new waiting to be activated
//		[Description("Inactive - Disabled due to Activity")]
//		Automated,	// Disabled due to activity - automated lockout - can be recovered via self service or auto-unlocked after period of time
//		[Description("Inactive - Locked by Security")]
//		Security		// Account is locked by security and cannote be recovered via self service or auto-unlocked
//	}
//}