using Real_Estate.Core.Application.ViewModels.Improvements;
using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Application.Interfaces.Services;
using AutoMapper;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Services
{
    public class ImprovementsService :
        GenericService<SaveImprovementsViewModel,
            ImprovementsViewModel, Improvements>, IImprovementsService
    {
        private readonly IGenericRepositoryAsync<Improvements> _repository;
        private readonly IMapper _mapper;

        public ImprovementsService(IGenericRepositoryAsync<Improvements> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
