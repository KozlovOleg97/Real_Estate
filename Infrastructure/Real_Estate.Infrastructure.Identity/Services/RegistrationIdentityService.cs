using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Domain.Settings;
using Real_Estate.Infrastructure.Identity.Contexts;
using Real_Estate.Infrastructure.Identity.Entities;

namespace Real_Estate.Infrastructure.Identity.Services
{
	public static class RegistrationIdentityService
	{
		public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			#region Contexts

			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				services.AddDbContext<IdentityContext>
					(options => options.UseInMemoryDatabase("IdentityDb"));
			}
			else
			{
				services.AddDbContext<IdentityContext>(options =>
				{
					options.EnableSensitiveDataLogging();

					options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
					migration => 
						migration.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
				});
			}

			#endregion

			#region Identity

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<IdentityContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/User";
				options.AccessDeniedPath = "/User/AccessDenied";
			});

			services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = configuration["JWTSettings:Issuer"],
					ValidAudience = configuration["JWTSettings:Audience"],
					IssuerSigningKey =
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
				};

				options.Events = new JwtBearerEvents()
				{
					OnAuthenticationFailed = consequences =>
					{
						consequences.NoResult();
						consequences.Response.StatusCode = 500;
						consequences.Response.ContentType = "text/plain";
						return consequences.Response.WriteAsync(consequences.Exception.ToString());
					},

					OnChallenge = consequences =>
					{
						consequences.HandleResponse();
						consequences.Response.StatusCode = 401;
						consequences.Response.ContentType = "application/json";
						var result = JsonConvert.SerializeObject(new JWTResponse
						{
							HasError = true,
							Error = "You are not Authorized"
						});
						return consequences.Response.WriteAsync(result);
					},
					OnForbidden = consequences =>
					{
						consequences.Response.StatusCode = 403;
						consequences.Response.ContentType = "application/json";
						var result = JsonConvert.SerializeObject(new JWTResponse
						{
							HasError = true,
							Error = "You are not Authorized to access this resource"
						});
						return consequences.Response.WriteAsync(result);
					}
				};
			});

			#endregion

			services.AddTransient<IAccountService, AccountService>();
		}
	}
}
