using System;
using c3o.Data;

namespace c3o.Web.Site.Data
{
    public interface IFolderData
    {
        void Add(string path);
        void Delete(string path);
        void DeletePermission(int folderId, int roleId);
        void DeletePermissions(int id);
        NullableDataReader Detail(long folderId);
        NullableDataReader Detail(string path);
        NullableDataReader EffectivePermisions(long sessionId, string path);
        NullableDataReader GroupPermissions(int folderId);
        NullableDataReader List();
        NullableDataReader Path(string path);
        NullableDataReader Permisions(int folderId);
        void Rename(string oldPath, string newPath);
        NullableDataReader SessionPermisions(long sessionId);
        NullableDataReader SessionGroupPermisions(long sessionId);
        void UpdateGroup(int folderId, long groupId, bool read, bool write, bool create, bool delete, bool admin, bool inherit);
        void UpdatePermission(int folderId, int roleId, bool read, bool write, bool create, bool delete, bool admin, bool inherit);
    }
}
