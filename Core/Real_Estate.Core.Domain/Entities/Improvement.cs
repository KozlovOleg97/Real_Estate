using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Domain.Common;

namespace Real_Estate.Core.Domain.Entities
{
	public class Improvement : OverallBaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
