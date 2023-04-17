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
	public static class DefaultDeveloperUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultDeveloper = new()
			{
				UserName = "developerUser",
				Email = "developer@email.com",
				FirstName = "Dev FirstName",
				LastName = "Dev LastName",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};

			if (userManager.Users.All(userDeveloper => userDeveloper.Id != defaultDeveloper.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultDeveloper, "DevPa$$word123");
					await userManager.AddToRoleAsync(defaultDeveloper, Roles.Developer.ToString());
				}
			}
		}
	}
}
