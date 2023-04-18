using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.ViewModels.Users
{
	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "Please, Try Again. You must enter your email")]
		[DataType(DataType.Text)]
		public string? Email { get; set; }

		public bool HasError { get; set; }
		public string? Error { get; set; }
	}
}
