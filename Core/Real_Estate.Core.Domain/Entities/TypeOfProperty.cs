using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Domain.Common;

namespace Real_Estate.Core.Domain.Entities
{
	public class TypeOfProperty : OverallBaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int PropertyId { get; set; }
		public Property MyProperty { get; set; }
	}
}
