using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c3o.Logger.Data
{
	public interface INameId
	{
		long Id { get; set; }
		string Name { get; set; }
	}
}
