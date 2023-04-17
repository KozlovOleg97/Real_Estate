using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Infrastructure.Identity.Entities;

namespace Real_Estate.Infrastructure.Identity.Seeds
{
	public static class DefaultRoles
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
		}
	} 
}
