using System;
using c3o.Data;
namespace c3o.Web.Site.Data
{
	public interface ISessionArchiveData
	{
		NullableDataReader Count(int siteId, long? personId, int? companyId, int? groupId);
		NullableDataReader Detail(long sessionId);
		NullableDataReader List(int siteId, long? personId, int? companyId, int? groupId);
	}
}
