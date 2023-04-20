using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Commands.CreateTypeOfProperties
{
    public class CreateTypeOfPropertiesCommand : IRequest<int>
    {
        //public int Id { get; set; }
        [SwaggerParameter(Description = "The name of the type of property")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "The description of the type of property")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }

    public class CreateTypeOfPropertiesCommandHandler : IRequestHandler<CreateTypeOfPropertiesCommand, int>
    {
        private readonly ITypeOfPropertiesRepository _typeOfPropertiesRepository;
        private readonly IMapper _mapper;
        public CreateTypeOfPropertiesCommandHandler(ITypeOfPropertiesRepository typeOfPropertiesRepository,
            IMapper mapper)
        {
            _typeOfPropertiesRepository = typeOfPropertiesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateTypeOfPropertiesCommand command, 
            CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Domain.Entities.TypeOfProperties>(command);

            property = await _typeOfPropertiesRepository.AddAsync(property);

            return property.Id;
        }
    }
}
