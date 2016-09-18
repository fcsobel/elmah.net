using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c3o.Core
{
	public static class ExceptionHelper
	{
		public static void Add(this Exception ex, string key, object data)
		{
			if (data != null)
			{
				ex.Data[key] = data;
			}
		}

		public static void Add(this Exception ex, string prefix, string name, object data)
		{
			if (data != null)
			{
				string key = string.Format("{0}.{1}", prefix, name);
				ex.Data[key] = data;
			}
		}
	}
}