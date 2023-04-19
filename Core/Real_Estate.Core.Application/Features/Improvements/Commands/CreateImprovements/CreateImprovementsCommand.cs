using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Features.Improvements.Commands.CreateImprovements
{
    public class CreateImprovementsCommand : IRequest<int>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
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

