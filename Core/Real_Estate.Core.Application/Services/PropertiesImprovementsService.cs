using AutoMapper;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.PropertiesImprovements;
using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Services
{
    public class PropertiesImprovementsService : GenericService<SavePropertiesImprovementsViewModel, 
        PropertiesImprovementsViewModel, PropertiesImprovements>, IPropertiesImprovementsService
    {
        private readonly IGenericRepositoryAsync<PropertiesImprovements> _repository;
        private readonly IMapper _mapper;
        public PropertiesImprovementsService(IGenericRepositoryAsync<PropertiesImprovements> repository, 
            IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
