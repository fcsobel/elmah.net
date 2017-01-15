using System;
using System.Collections.Generic;
//using Microsoft.AspNet.Identity;

namespace Elmah.Net.Logger.Data
{
	public partial class Role // : IRole<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		//public virtual ICollection<Person> People { get; set; }

		public Role()
		{
			// this.People = new List<Person>();
		}
	}
}