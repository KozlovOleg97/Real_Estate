using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.ViewModels.Admin
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string? IDCard { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }
        public string? PropertiesQuantity { get; set; }
    }
}
