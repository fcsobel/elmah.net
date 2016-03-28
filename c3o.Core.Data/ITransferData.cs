using System;
using System.Data;

namespace c3o.Web.Site.Data
{
	public interface ITransferData
	{
		long StartTransfer(long sessionId, string path, string name, string extension, long size, string fileowner, string direction);
        long StartTransferUp(long sessionId, string path, string name, string extension, long size, string fileowner);
        long StartTransferDown(long sessionId, string path, string name, string extension, long size, string fileowner);
		void UpdateTransfer(long transferId, long bytes, string status);
	}
}
