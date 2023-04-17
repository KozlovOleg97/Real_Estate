using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Domain.Common;

namespace Real_Estate.Core.Domain.Entities
{
	public class Property : OverallBaseEntity
	{
		public int Code { get; set; }
		public decimal Price { get; set; }
		public int LandSize { get; set; }
		public int NumberOfRooms { get; set; }
		public int NumberOfBathrooms { get; set; }
		public string Description { get; set; }
		public string AgentName { get; set; }
		public int AgentId { get; set; }
		public ICollection<Improvement> Improvements { get; set; }
		public TypeOfProperty TypeOfProperty { get; set; }
		public TypeOfSale TypeOfSale { get; set; }
	}
}
