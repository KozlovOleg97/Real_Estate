using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Application.DTOs.Email;
using Real_Estate.Core.Domain.Settings;

namespace Real_Estate.Core.Application.Interfaces.Services
{
	public interface IEmailService
	{
		public MailSettings _mailSettings { get; }
		Task SendEmailAsync(EmailRequest request);
	}
}
