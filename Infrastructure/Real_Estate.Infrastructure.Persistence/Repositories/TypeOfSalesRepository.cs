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
    public class TypeOfSalesRepository : GenericRepository<TypeOfSales>, ITypeOfSalesRepository
    {
        private readonly ApplicationContext _dbContext;

        public TypeOfSalesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
