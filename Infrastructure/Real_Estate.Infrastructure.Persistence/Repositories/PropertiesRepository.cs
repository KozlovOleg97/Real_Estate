using Microsoft.EntityFrameworkCore;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Domain.Entities;
using Real_Estate.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Real_Estate.Infrastructure.Persistence.Repositories
{
    public class PropertiesRepository : GenericRepository<Properties>, IPropertiesRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropertiesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddImprovementsToProperties(Properties property)
        {
            PropertiesImprovements propertiesImprovements = new PropertiesImprovements();
            propertiesImprovements.PropertyId = property.Id;

            List<PropertiesImprovements> propertiesImprovementsList = new List<PropertiesImprovements>();

            foreach (var item in property.Improvements)
            {
                propertiesImprovements.ImprovementId = item.Id;
                propertiesImprovementsList.Add(propertiesImprovements);

                await _dbContext.PropertiesImprovements.AddAsync(propertiesImprovements);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateImprovementsToProperties(Properties property)
        {

            List<PropertiesImprovements> allPropertiesImprovementsList = await _dbContext.PropertiesImprovements.ToListAsync();

            foreach (var item in allPropertiesImprovementsList)
            {
                if (item.PropertyId == property.Id)
                {
                    _dbContext.PropertiesImprovements.Remove(item);
                }
            }
            await AddImprovementsToProperties(property);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteImprovementsToProperties(int id)
        {
            List<PropertiesImprovements> allPropertiesImprovementsList = await _dbContext.PropertiesImprovements.ToListAsync();

            foreach (var item in allPropertiesImprovementsList)
            {
                if (item.PropertyId == id)
                {
                    _dbContext.PropertiesImprovements.Remove(item);

                }
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
