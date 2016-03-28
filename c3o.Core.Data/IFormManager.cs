using System;
using System.Collections.Generic;
using c3o.Common;

namespace c3o.Web.Site.Data
{
    public interface IFormManager
    {
        FormFileObject FormDetail(int fileId);
        List<FormFileObject> FormHistory(string formType, string status, int? groupId, long? personId, int? formId, int limit);
        List<FormObject> FormList();
        List<FormObject> FormList(string formType);
        FormFileObject UpdateForm(long sessionId, FormFileObject obj);
    }
}
