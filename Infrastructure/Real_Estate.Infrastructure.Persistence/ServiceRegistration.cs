using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Infrastructure.Persistence.Contexts;
using Real_Estate.Infrastructure.Persistence.Repositories;

namespace Real_Estate.Infrastructure.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
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
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }

			#endregion

			#region Repositories

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepository<>));
            services.AddTransient<IImprovementsRepository, ImprovementsRepository>();
            services.AddTransient<ITypeOfPropertiesRepository, TypeOfPropertiesRepository>();
            services.AddTransient<ITypeOfSalesRepository, TypeOfSalesRepository>();
            services.AddTransient<IPropertiesRepository, PropertiesRepository>();

            #endregion
        }
	}
}
