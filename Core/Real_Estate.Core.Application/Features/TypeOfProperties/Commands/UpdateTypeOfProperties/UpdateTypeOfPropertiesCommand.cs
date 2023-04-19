using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeOfProperties
{
    public class UpdateTypeOfPropertiesCommand : IRequest<UpdateTypeOfPropertiesResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTypeOfPropertiesCommandHandler : IRequestHandler<UpdateTypeOfPropertiesCommand, 
        UpdateTypeOfPropertiesResponse>
    {
        private readonly ITypeOfPropertiesRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public UpdateTypeOfPropertiesCommandHandler(ITypeOfPropertiesRepository improvementRepository, 
            IMapper mapper)
        {
            _improvementsRepository = improvementRepository;
            _mapper = mapper;
        }
        public async Task<UpdateTypeOfPropertiesResponse> Handle(UpdateTypeOfPropertiesCommand command, 
            CancellationToken cancellationToken)
        {
            var improvement = await _improvementsRepository.GetByIdAsync(command.Id);

            if (improvement == null) 
                throw new Exception("Property type Not Found.");

            improvement = _mapper.Map<Domain.Entities.TypeOfProperties>(command);

            await _improvementsRepository.UpdateAsync(improvement, improvement.Id);

            var improvementResponse = _mapper.Map<UpdateTypeOfPropertiesResponse>(improvement);

            return improvementResponse;
        }
    }
}
