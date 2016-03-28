using System;
using System.Collections.Generic;
using System.IO;
using c3o.Common;
using System.ServiceModel;

namespace c3o.Web.Site.Data
{
	public interface IDiskManager
	{
		DirectoryFileStatus CreateDirectory(string path);
		DirectoryFileStatus DeleteDirectory(string path);
		DirectoryFileStatus DeleteFile(string path);
		DirectoryFileStatus MoveDirectory(string source, string destination);
		DirectoryFileStatus MoveFile(string source, string destination);

		FolderObject FolderDetail(string root, string path);
		List<FolderObject> GetDirectories(string path, string searchPattern, SearchOption searchOption, bool includeRoot);
		List<FolderObject> GetFiles(string path, string searchPattern, SearchOption searchOption);
		List<FolderObject> List(string path, string searchPattern, SearchOption searchOption);

        [OperationContract]
        void SetFileAttribute(string fullFilePath, FileAttributes fileAttribute);
	}
}
