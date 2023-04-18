using Real_Estate.Core.Application.ViewModels.Properties;
using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Services
{
    public interface IPropertiesService : IGenericService<SavePropertiesViewModel, 
        PropertiesViewModel, Properties>
    {
        Task<SaveAgentProfileViewModel> UpdateAgentProfile(SaveAgentProfileViewModel agentProfileViewModel);

        Task<SaveAgentProfileViewModel> GetAgentUserByUserNameAsync(string userName);
    }
}
