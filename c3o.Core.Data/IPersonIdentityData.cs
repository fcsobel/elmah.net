using System;
namespace c3o.Web.Site.Data
{
    public interface IPersonIdentityData
    {
        void Add(long personId, string provider, string providerKey);
        void Delete(long personId, string provider, string providerKey);
        c3o.Data.NullableDataReader Detail(string provider, string providerKey);
        c3o.Data.NullableDataReader List(long personId);
    }
}
