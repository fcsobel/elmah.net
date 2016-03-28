using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IPersonContactData
	{
		void Add(long personId, string email, string phoneOffice, string phoneOfficeExt, string fax, string phoneMobile, string phoneHome, string imHandle, string imService);
		void Delete(long id);
		void Update(long personId, string email, string phoneOffice, string phoneOfficeExt, string fax, string phoneMobile, string phoneHome, string imHandle, string imService);
		NullableDataReader Detail(long id);
		NullableDataReader List();
	}
}
