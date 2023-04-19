using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Features.Improvements.Commands.DeleteImprovomentsById
{
    public class DeleteImprovementsByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteImprovementsByIdCommandHandler : 
        IRequestHandler<DeleteImprovementsByIdCommand, int>
    {
        private readonly IImprovementsRepository _improvementsRepository;

        public DeleteImprovementsByIdCommandHandler(IImprovementsRepository improvementsRepository)
        {
            _improvementsRepository = improvementsRepository;
        }

        public async Task<int> Handle(DeleteImprovementsByIdCommand command, CancellationToken cancellationToken)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(command.Id);

            if (improvements == null)
                throw new Exception("Improvements Not Found.");

            await _improvementsRepository.DeleteAsync(improvements);

            return improvements.Id;
        }
    }
}
