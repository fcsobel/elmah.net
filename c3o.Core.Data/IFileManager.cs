using System;
using System.Collections.Generic;
using c3o.Common;

namespace c3o.Web.Site.Data
{
	public interface IFileManager
	{
		//DirectoryFileStatus CreateDirectory(string newDirName);
		//DirectoryFileStatus DeleteDirectory(string DirName);
		//DirectoryFileStatus DeleteFile(string FilePath);
		//DirectoryFileStatus MoveDirectory(string sourceName, string destName);
		//DirectoryFileStatus MoveFile(string sourceName, string destName);

		FileObject Detail(int fileId);
		FileObject Detail(string path);
		void Delete(long sessionId, int id);

		//List<FolderObject> GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption, bool includeRoot);
		List<FolderObject> GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption, bool includeRoot, long sessionId);

		//List<FolderObject> GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);
		
		List<FileObject> List();
		//List<FolderObject> List(string path, string searchPattern, System.IO.SearchOption searchOption);
		List<FolderObject> List(string path, string searchPattern, System.IO.SearchOption searchOption, long sessionId);
		int Update(long sessionId, FileObject file);
	}
}
