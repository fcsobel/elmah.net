using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface ILookupData
    {
        NullableDataReader List();
        NullableDataReader List(string TypeName);
        NullableDataReader ListOld(string TypeName);
    }
}
