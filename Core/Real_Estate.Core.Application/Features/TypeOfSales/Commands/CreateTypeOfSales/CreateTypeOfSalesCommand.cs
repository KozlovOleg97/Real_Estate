using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Domain.Entities;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.TypeOfSales.Commands.CreateTypeOfSales
{
    public class CreateTypeOfSalesCommand : IRequest<int>
    {
        public int Id { get; set; }
        [SwaggerParameter(Description = "The name of the type of sale")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "The description of the type of sale")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }

    public class CreateTypeOfSalesCommandHandler : IRequestHandler<CreateTypeOfSalesCommand, int>
    {
        private readonly ITypeOfSalesRepository _typeOfSalesRepository;
        private readonly IMapper _mapper;
        public CreateTypeOfSalesCommandHandler(ITypeOfSalesRepository typeOfSalesRepository, 
            IMapper mapper)
        {
            _typeOfSalesRepository = typeOfSalesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateTypeOfSalesCommand command, 
            CancellationToken cancellationToken)
        {
            var improvements = _mapper.Map<Domain.Entities.TypeOfSales>(command);

            improvements = await _typeOfSalesRepository.AddAsync(improvements);

            return improvements.Id;
        }
    }
}
