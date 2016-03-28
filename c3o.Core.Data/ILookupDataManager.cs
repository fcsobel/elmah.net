using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data 
{
    public interface ILookupDataManager
    {
        List<LookupObject> List();
        List<LookupObject> List(string typeName);
        List<LookupValue> ListOld(string typeName);
    }
}
