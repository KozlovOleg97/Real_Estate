using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.Services;

namespace Real_Estate.Core.Application
{
	public static class ServiceRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            services.AddTransient<IImprovementsService, ImprovementsService>();

            services.AddTransient<ITypeOfPropertiesService, TypeOfPropertiesService>();

            services.AddTransient<ITypeOfSalesService, TypeOfSalesService>();

            services.AddTransient<IUserService, UserService>();

			#endregion
		}
	}
}
