using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Repositories
{
    public interface IPropertiesRepository : IGenericRepositoryAsync<Properties>
    {
        Task AddImprovementsToProperties(Properties property);
        Task UpdateImprovementsToProperties(Properties property);
        Task DeleteImprovementsToProperties(int id);
    }
}
