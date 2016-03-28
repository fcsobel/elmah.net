using System;
using System.Collections.Generic;
using c3o.Common;
namespace c3o.Web.Site.Data
{
	public interface IFolderManager
	{
		DirectoryFileStatus Create(string path);
		DirectoryFileStatus Delete(FolderObject obj);
		FolderObject Detail(int id);
		FolderObject Detail(string path);
		List<FolderObject> List();
		DirectoryFileStatus Move(string sourcePath, string destPath);
		List<FolderObject> Path(string path);
		//List<FolderPermissionObject> Permissions(long sessionId);
		//List<GroupPermissionObject> PersonPermissions(int folderId);
		FolderObject Update(FolderObject folder);
		void UpdateGroup(int folderId, long groupId, bool read, bool write, bool create, bool delete, bool admin, bool inherit);
		void UpdateGroup(string path, long groupId, bool read, bool write, bool create, bool delete, bool admin, bool inherit);
		FolderObject GetPermission(FolderObject folder, long sessionId);
		List<FolderObject> GetPermissions(List<FolderObject> list, long sessionId);
	}


	//public enum DirectoryFileStatus
	//{
	//    CreatedSuccessfully,
	//    DeletedSuccessfully,
	//    DirectoryNotEmpty,
	//    DirectoryNotFound,
	//    AlreadyExists,
	//    InvalidFileExtension,
	//    InvalidCharacters,
	//    FileSizeOverLimit,
	//    FileInaccessible,
	//    GenericFailure,
	//    NotificationError
	//}
}
