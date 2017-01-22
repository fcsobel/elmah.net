using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Elmah.Net
{
	public static class StringHelper
	{
		public static string CheckSum(string data)
		{
			using (var md5 = MD5.Create())
			{
				return Encoding.Default.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(data)));
			}
		}

		public static string Compress(string s)
		{
			var bytes = Encoding.Unicode.GetBytes(s);
			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(mso, CompressionMode.Compress))
				{
					msi.CopyTo(gs);
				}
				return Convert.ToBase64String(mso.ToArray());
			}
		}

		public static string Decompress(string s)
		{
			var bytes = Convert.FromBase64String(s);
			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(msi, CompressionMode.Decompress))
				{
					gs.CopyTo(mso);
				}
				return Encoding.Unicode.GetString(mso.ToArray());
			}
		}
	}
}
