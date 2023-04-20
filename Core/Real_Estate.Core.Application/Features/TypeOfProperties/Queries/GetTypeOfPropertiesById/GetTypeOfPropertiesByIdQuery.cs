using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.TypeOfProperties;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetTypeOfPropertiesById
{
    public class GetTypeOfPropertiesByIdQuery : IRequest<TypeOfPropertiesViewModel>
    {
        [SwaggerParameter(Description = "The ID of the type of property you want to consult")]
        public int Id { get; set; }
    }

    public class GetTypeOfPropertiesByIdQueryHandler : IRequestHandler<GetTypeOfPropertiesByIdQuery, 
        TypeOfPropertiesViewModel>
    {
        private readonly ITypeOfPropertiesRepository _TypeOfPropertiesRepository;
        private readonly IMapper _mapper;

        public GetTypeOfPropertiesByIdQueryHandler(ITypeOfPropertiesRepository TypeOfPropertiesRepository,
            IMapper mapper)
        {
            _TypeOfPropertiesRepository = TypeOfPropertiesRepository;
            _mapper = mapper;
        }

        public async Task<TypeOfPropertiesViewModel> Handle(GetTypeOfPropertiesByIdQuery query,
            CancellationToken cancellationToken)
        {
            var typeOfProperty = await _TypeOfPropertiesRepository.GetByIdAsync(query.Id);

            if (typeOfProperty is null) 
                throw new Exception("Type Of Property Not Found");

            var result = _mapper.Map<TypeOfPropertiesViewModel>(typeOfProperty);

            return result;
        }
    }
}
