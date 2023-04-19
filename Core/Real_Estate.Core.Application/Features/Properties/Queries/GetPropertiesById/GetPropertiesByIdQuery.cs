﻿using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Features.Properties.Queries.GetPropertiesById
{
    public class GetPropertiesByIdQuery : IRequest<PropertiesViewModel>
    {
        public int Id { get; set; }
    }
    public class GetPropertiesByIdQueryHandler : IRequestHandler<GetPropertiesByIdQuery, PropertiesViewModel>
    {
        private readonly IPropertiesRepository _PropertiesRepository;
        private readonly IMapper _mapper;

        public GetPropertiesByIdQueryHandler(IPropertiesRepository PropertiesRepository, IMapper mapper)
        {
            _PropertiesRepository = PropertiesRepository;
            _mapper = mapper;
        }

        public async Task<PropertiesViewModel> Handle(GetPropertiesByIdQuery query, CancellationToken cancellationToken)
        {
            var property = await _PropertiesRepository.GetByIdAsync(query.Id);

            if (property is null) 
                throw new Exception("Property Not Found");

            var result = _mapper.Map<PropertiesViewModel>(property);

            return result;
        }
    }
}