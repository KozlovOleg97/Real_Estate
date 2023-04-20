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

namespace Real_Estate.Core.Application.Features.TypeOfSales.Commands.UpdateTypeOfSales
{
    public class TypeOfSalesUpdateCommand : IRequest<TypeOfSalesUpdateResponse>
    {
        public int Id { get; set; }

        [SwaggerParameter(Description = "The name of the type of sale")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "The description of the type of sale")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
    public class UpdateTypeOfSalesCommandHandler : IRequestHandler<TypeOfSalesUpdateCommand, 
        TypeOfSalesUpdateResponse>
    {
        private readonly ITypeOfSalesRepository _typeOfSalesRepository;
        private readonly IMapper _mapper;
        public UpdateTypeOfSalesCommandHandler(ITypeOfSalesRepository improvementRepository, 
            IMapper mapper)
        {
            _typeOfSalesRepository = improvementRepository;
            _mapper = mapper;
        }
        public async Task<TypeOfSalesUpdateResponse> Handle(TypeOfSalesUpdateCommand command, 
            CancellationToken cancellationToken)
        {
            var improvement = await _typeOfSalesRepository.GetByIdAsync(command.Id);

            if (improvement == null) 
                throw new Exception("Type of Sale hasn't been found.");

            improvement = _mapper.Map<Domain.Entities.TypeOfSales>(command);

            await _typeOfSalesRepository.UpdateAsync(improvement, improvement.Id);

            var improvementResponse = _mapper.Map<TypeOfSalesUpdateResponse>(improvement);

            return improvementResponse;
        }
    }
}
