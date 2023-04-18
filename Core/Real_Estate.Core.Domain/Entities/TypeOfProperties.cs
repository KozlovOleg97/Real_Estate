using Real_Estate.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Domain.Entities
{
    public class TypeOfProperties : OverallBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Properties>? Properties { get; set; }
    }
}
