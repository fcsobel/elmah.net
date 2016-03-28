using System.IO;
using System.ServiceModel;
using c3o.Common;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
	[ServiceContract]
	public interface ITransferManager
	{
        [OperationContract]
        bool Exists(FolderRoot root, string path);

        [OperationContract(Name = "FileExists")]
        bool Exists(FolderRoot root, string path, string filename);

        [OperationContract(Name = "FolderDetail")]
        FolderObject Detail(FolderRoot root, string path);

        [OperationContract(Name = "FolderDetailForSession")]
        FolderObject Detail(FolderRoot root, string path, long sessionId);

        [OperationContract(Name = "FileDetail")]
        FolderObject Detail(FolderRoot root, string path, string filename);

        [OperationContract(Name = "FileDetailForSession")]
        FolderObject Detail(FolderRoot root, string path, string filename, long sessionId);

        [OperationContract]
        DirectoryFileStatus Move(FolderRoot root, string sourcePath, string destPath);

        [OperationContract(Name = "MoveFile")]
        DirectoryFileStatus Move(FolderRoot root, string sourcePath, string destPath, string filename);

        [OperationContract]
        DirectoryFileStatus Rename(FolderRoot root, string path, string fileName, string newfilename);

        [OperationContract]
        DirectoryFileStatus Delete(FolderRoot root, string path);

        [OperationContract(Name = "DeleteFile")]
        DirectoryFileStatus Delete(FolderRoot root, string path, string filename);

        [OperationContract]
        DirectoryFileStatus Create(FolderRoot root, string path);
	
        [OperationContract]
        byte[] Read(FolderRoot root, string path, string filename);

		[OperationContract(Name = "ReadFiles")]
		byte[] Read(FolderRoot root, string path, List<string> files);

		[OperationContract(Name = "ReadFiles2")]
		byte[] Read(FolderRoot root, string path, List<FolderObject> files);
						
		[OperationContract(Name = "ReadChunk")]
        byte[] Read(FolderRoot root, string path, string filename, long transferId, long position, long length);

        [OperationContract]
        DirectoryFileStatus Write(FolderRoot root, string path, string filename, byte[] data);

        [OperationContract(Name = "WriteChunk")]
        DirectoryFileStatus Write(FolderRoot root, string path, string filename, long transferId, byte[] buffer, int length, bool firstChunk, bool lastChunk);


        //[OperationContract]
        //string GetPath(FolderRoot root);

        //[OperationContract(Name = "FolderPath")]
        //string GetPath(FolderRoot root, string path);

        //[OperationContract(Name = "FilePath")]
        //string GetPath(FolderRoot root, string path, string filename);
    }
}