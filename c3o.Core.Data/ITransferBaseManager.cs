using System.IO;
using System.ServiceModel;
using c3o.Common;

namespace c3o.Web.Site.Data
{
	[ServiceContract]
	public interface ITransferBaseManager
	{
        [OperationContract]
        bool Exists(FolderRoot root, string path);

        [OperationContract(Name = "FileExists")]
        bool Exists(FolderRoot root, string path, string filename);

        [OperationContract]
        FolderObject Detail(FolderRoot root, string path);

        [OperationContract(Name = "FileDetail")]
        FolderObject Detail(FolderRoot root, string path, string filename);

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

        [OperationContract(Name = "ReadChunk")]
        byte[] Read(FolderRoot root, string path, string filename, long position, long length);

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