using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Domain.Common
{
	public class OverallBaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? Created { get; set; }
		public string? LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
	}
}
