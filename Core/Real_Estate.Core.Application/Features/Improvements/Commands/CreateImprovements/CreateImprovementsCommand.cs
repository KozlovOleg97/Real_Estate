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

namespace Real_Estate.Core.Application.Features.Improvements.Commands.CreateImprovements
{
    public class CreateImprovementsCommand : IRequest<int>
    {
        //public int Id { get; set; }

        [SwaggerParameter(Description = "The name of the improvement")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "The description of the improvement")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }

    public class CreateImprovementsCommandHandler : IRequestHandler<CreateImprovementsCommand, int>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public CreateImprovementsCommandHandler(IImprovementsRepository improvementsRepository, 
            IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateImprovementsCommand command, 
            CancellationToken cancellationToken)
        {
            var improvements = _mapper.Map<Domain.Entities.Improvements>(command);

            improvements = await _improvementsRepository.AddAsync(improvements);

            return improvements.Id;
        }
    }
}

