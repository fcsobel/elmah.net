using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IFormData
    {
        NullableDataReader List();
        NullableDataReader List(string formType);
        NullableDataReader Detail(int formId);
        NullableDataReader Detail(string formPath);
    }
}
