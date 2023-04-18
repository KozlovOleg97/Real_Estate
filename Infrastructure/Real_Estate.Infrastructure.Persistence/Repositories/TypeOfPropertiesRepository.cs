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
    public class TypeOfPropertiesRepository : GenericRepository<TypeOfProperties>, 
        ITypeOfPropertiesRepository
    {
        private readonly ApplicationContext _dbContext;

        public TypeOfPropertiesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
