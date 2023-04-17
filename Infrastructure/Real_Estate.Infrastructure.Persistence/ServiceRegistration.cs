using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Infrastructure.Persistence.Contexts;

namespace Real_Estate.Infrastructure.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			#region Contexts

			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				services.AddDbContext<ApplicationContext>(options => 
					options.UseInMemoryDatabase("ApplicationDb"));
			}
			else
			{
				services.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
						migration => migration.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
			}
			#endregion

			#region Repositories
			#endregion
		}
	}
}
