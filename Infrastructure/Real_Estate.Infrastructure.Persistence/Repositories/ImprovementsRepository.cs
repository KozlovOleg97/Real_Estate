using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Domain.Entities;
using Real_Estate.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Infrastructure.Persistence.Repositories
{
    public class ImprovementsRepository : GenericRepository<Improvements>, IImprovementsRepository
    {
        private readonly ApplicationContext _dbContext;

        public ImprovementsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
