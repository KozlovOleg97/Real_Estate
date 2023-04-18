using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Domain.Common;

namespace Real_Estate.Core.Domain.Entities
{
    public class Agents : OverallBaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public int NumberOfProperties { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Properties>? Properties { get; set; }
    }
}
