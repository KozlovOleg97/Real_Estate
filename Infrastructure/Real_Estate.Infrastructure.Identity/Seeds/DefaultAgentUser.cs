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
	public static class DefaultAgentUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultAgent = new()
			{
				FirstName = "Jack",
				LastName = "Campble",
				UserName = "Jack",
				Email = "Jack.Agent@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "+375295435704",
				PhoneNumberConfirmed = true
			};

			if (userManager.Users.All(userAgent => userAgent.Id != defaultAgent.Email))
			{
				var user = await userManager.FindByEmailAsync(defaultAgent.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultAgent, "SimplePa$$word12345");
					await userManager.AddToRoleAsync(defaultAgent, Roles.Admin.ToString());
				}
			}
		}
	}
}
