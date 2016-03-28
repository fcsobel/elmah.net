using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IFileData
    {
        int Update(long sessionID, Nullable<int> id, string name, string path, long size, string fileType);
        void Delete(long sessionID, int id);
        NullableDataReader List();
        NullableDataReader Detail(int fileId);
        NullableDataReader Detail(string path);
    }
}
