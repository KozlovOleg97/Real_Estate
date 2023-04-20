using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.Properties;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.Properties.Queries.GetPropertiesById
{
    public class GetPropertiesByCodeQuery : IRequest<PropertiesViewModel>
    {
        [SwaggerParameter(Description = "The code of the property that you want to consult")]
        public string Code { get; set; }
    }

    public class GetPropertiesByCodeQueryHandler : IRequestHandler<GetPropertiesByCodeQuery, PropertiesViewModel>
    {
        private readonly IPropertiesRepository _PropertiesRepository;
        private readonly IMapper _mapper;

        public GetPropertiesByCodeQueryHandler(IPropertiesRepository PropertiesRepository, IMapper mapper)
        {
            _PropertiesRepository = PropertiesRepository;
            _mapper = mapper;
        }

        public async Task<PropertiesViewModel> Handle(GetPropertiesByCodeQuery query, CancellationToken cancellationToken)
        {
            var properties = await _PropertiesRepository.GetAllAsync();

            var property = properties.FirstOrDefault(x => x.Code == query.Code);

            if (property is null) 
                throw new Exception("There are not property.");

            var result = await _PropertiesRepository.GetAllWithIncludeAsync(
                new List<string> { "Improvements", "TypeOfProperty", "TypeOfSale" });

            return _mapper.Map<PropertiesViewModel>(property);
        }
    }
}
