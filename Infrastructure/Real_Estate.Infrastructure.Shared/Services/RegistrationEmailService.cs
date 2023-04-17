using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Domain.Settings;

namespace Real_Estate.Infrastructure.Shared.Services
{
	public static class RegistrationEmailService
	{
		public static void AddSharedInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
			services.AddTransient<IEmailService, EmailService>();
		}
	}
}
