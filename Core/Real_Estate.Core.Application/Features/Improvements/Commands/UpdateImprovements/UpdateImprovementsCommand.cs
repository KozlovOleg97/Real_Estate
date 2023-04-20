using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Features.Improvements.Commands.UpdateImprovements
{
    
    public class UpdateImprovementsCommand : IRequest<UpdateImprovementsResponse>
    {
        public int Id { get; set; }

        [SwaggerParameter(Description = "The name of the improvement")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "The description of the improvement")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }

    public class UpdateImprovementsCommandHandler : IRequestHandler<UpdateImprovementsCommand, 
        UpdateImprovementsResponse>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public UpdateImprovementsCommandHandler(IImprovementsRepository improvementRepository, 
            IMapper mapper)
        {
            _improvementsRepository = improvementRepository;
            _mapper = mapper;
        }
        public async Task<UpdateImprovementsResponse> Handle(UpdateImprovementsCommand command, 
            CancellationToken cancellationToken)
        {
            var improvement = await _improvementsRepository.GetByIdAsync(command.Id);

            if (improvement == null) 
                throw new Exception("The Improvement hasn't been found.");

            improvement = _mapper.Map<Domain.Entities.Improvements>(command);

            await _improvementsRepository.UpdateAsync(improvement, improvement.Id);

            var improvementResponse = _mapper.Map<UpdateImprovementsResponse>(improvement);

            return improvementResponse;
        }
    }
}
