using System;
using System.Net;

namespace c3o.Core
{
    public static class NetworkHelper
	{
		/// <summary>
		/// Check for public IP
		/// private:	192.168.0.0 - 192.168.255.255			
		///				172.16.0.0 - 172.31.255.255
		///				10.0.0.0 - 10.255.255.255 
		///	loopback:	127.0.0.1
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns></returns>
		public static bool Public(string ipAddress)
		{
			if (string.IsNullOrWhiteSpace(ipAddress)) return false; // empty			
			if (ipAddress == "127.0.0.1") return false; // localhost

			var nodes = ipAddress.ParseList<int>('.');
			if (nodes.Count != 4) return false; // invalid

			var node0 = nodes[0];
			var node1 = nodes[1];

			// check private ranges
			return !(node0 == 10										// 10.0.0.0		- 10.255.255.255 
					|| (node0 == 192 && node1 == 168)					// 192.168.0.0	- 192.168.255.255			 
					|| (node0 == 172 && (node1 >= 16 && node1 <= 31))	// 172.16.0.0	- 172.31.255.255
					);
		}

		public static Int64 IPv4StringToInt64(string ipv4String)
		{
			Int64 ipInteger = 0;
			try
			{
				ipInteger = (long)(uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(IPAddress.Parse(ipv4String).GetAddressBytes(), 0));
			}
			catch (Exception)
			{ }

			return ipInteger;
		}


		public static string IPv4Int64ToString(Int64 ipv4Int64)
		{
			if (ipv4Int64 < 0)
				return "0.0.0.0";
			else if (ipv4Int64 < 4294967295)
				return IPAddress.Parse(ipv4Int64.ToString()).ToString();
			else
				return "255.255.255.255";
		}
	}
}
