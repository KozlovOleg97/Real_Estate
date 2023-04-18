using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.ViewModels.Admin
{
    public class HomeAdminViewModel
    {
        public int PropertiesQuantity { get; set; }
        public int ActiveAgentsQuantity { get; set; }
        public int UnactiveAgentsQuantity { get; set; }
        public int ActiveClientsQuantity { get; set; }
        public int UnactiveClientsQuantity { get; set; }
        public int ActiveDevsQuantity { get; set; }
        public int UnactiveDevsQuantity { get; set; }
    }
}
