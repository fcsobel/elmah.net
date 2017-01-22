using System;
using System.Net;

namespace Elmah.Net
{
    public static class NetworkHelper
	{
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
			if (ipv4Int64 < 0) return "0.0.0.0";
			else if (ipv4Int64 < 4294967295) return IPAddress.Parse(ipv4Int64.ToString()).ToString();
			else return "255.255.255.255";
		}
	}
}