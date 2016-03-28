using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IFormFileData
    {
        NullableDataReader List(string formType, string status, int? groupId, long? personId, int? formId, int limit);
        void Update(long sessionId, int formId, int fileId, int groupId, string status);
        NullableDataReader Detail(int fileId);
    }
}
