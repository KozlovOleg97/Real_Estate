using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.DTOs.Account
{
    public class ChangeUserStatusResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
