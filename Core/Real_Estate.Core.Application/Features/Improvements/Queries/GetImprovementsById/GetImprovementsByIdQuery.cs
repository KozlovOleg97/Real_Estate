﻿using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.Improvements;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Features.Improvements.Queries.GetImprovementsById
{
    public class GetImprovementsByIdQuery : IRequest<ImprovementsViewModel>
    {
        [SwaggerParameter(Description = "The Id of the improvement you want to consult")]
        public int Id { get; set; }
    }

    public class GetImprovementsByIdQueryHandler : IRequestHandler<GetImprovementsByIdQuery, 
        ImprovementsViewModel>
    {
        private readonly IImprovementsRepository _ImprovementsRepository;
        private readonly IMapper _mapper;

        public GetImprovementsByIdQueryHandler(IImprovementsRepository ImprovementsRepository, 
            IMapper mapper)
        {
            _ImprovementsRepository = ImprovementsRepository;
            _mapper = mapper;
        }

        public async Task<ImprovementsViewModel> Handle(GetImprovementsByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var improvement = await _ImprovementsRepository.GetByIdAsync(query.Id);

            if (improvement is null) 
                throw new Exception("Improvement Not Found");

            var result = _mapper.Map<ImprovementsViewModel>(improvement);

            return result;
        }
    }
}
