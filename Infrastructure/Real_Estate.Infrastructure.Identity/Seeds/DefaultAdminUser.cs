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
	public static class DefaultAdminUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultAdmin = new()
			{
				UserName = "adminUser",
				Email = "admin@email.com",
				FirstName = "Admin FirstName",
				LastName = "Admin LastName",
                ImagePath = "/Images/profile.jpeg",
                EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};

			if (userManager.Users.All(userAdmin => userAdmin.Id != defaultAdmin.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultAdmin, "SimplePa$$word12345");
					await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
				}
			}
		}
	}
}
