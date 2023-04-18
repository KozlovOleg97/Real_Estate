using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Real_Estate.Infrastructure.Identity.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
        //public string? IDCard { get; set; }
        public string? ImagePath { get; set; }
	}
}
