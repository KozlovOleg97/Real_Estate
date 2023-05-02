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
	public static class DefaultClientUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultClient = new()
			{
				FirstName = "Brett",
				LastName = "Ratner",
				UserName = "Brett",
				Email = "Brett.Client@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "+375295435704",
				PhoneNumberConfirmed = true
			};

			if (userManager.Users.All(userClient => userClient.Id != defaultClient.Email))
			{
				var user = await userManager.FindByEmailAsync(defaultClient.Email);

				if (user == null)
				{
					await userManager.CreateAsync(defaultClient, "SimplePa$$word12345");
					await userManager.AddToRoleAsync(defaultClient, Roles.Admin.ToString());
				}
			}
		}
	}
}
