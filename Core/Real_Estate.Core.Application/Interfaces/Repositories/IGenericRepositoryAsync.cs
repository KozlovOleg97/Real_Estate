﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
    }
}
