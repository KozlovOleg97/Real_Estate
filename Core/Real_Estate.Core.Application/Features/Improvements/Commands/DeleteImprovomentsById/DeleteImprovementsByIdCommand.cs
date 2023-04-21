using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Features.Improvements.Commands.DeleteImprovomentsById
{
    public class DeleteImprovementsByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "The Id of the enhancement you want to remove")]
        public int Id { get; set; }
    }

    public class DeleteImprovementsByIdCommandHandler : 
        IRequestHandler<DeleteImprovementsByIdCommand, int>
    {
        private readonly IImprovementsRepository _improvementsRepository;

        private readonly IPropertiesRepository _propertiesRepository;
        private readonly IPropertiesImprovementsRepository _propertiesImprovementsRepository;

        public DeleteImprovementsByIdCommandHandler(IImprovementsRepository improvementsRepository, 
            IPropertiesRepository propertiesRepository, IPropertiesImprovementsRepository propertiesImprovementsRepository)
        {
            _improvementsRepository = improvementsRepository;
            _propertiesRepository = propertiesRepository;
            _propertiesImprovementsRepository = propertiesImprovementsRepository;
        }

        public async Task<int> Handle(DeleteImprovementsByIdCommand command, 
            CancellationToken cancellationToken)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(command.Id);

            if (improvements == null)
                throw new Exception("Improvements Not Found.");

            var improvementsProperties = await _propertiesImprovementsRepository.GetAllAsync();

            var improvementList = improvementsProperties.Where(
                x => x.ImprovementId == command.Id);

            if (improvementList is not null)
            {
                foreach (var improvement in improvementList)
                {
                    await _propertiesImprovementsRepository.DeleteAsync(improvement);
                }
            }

            await _improvementsRepository.DeleteAsync(improvements);

            return improvements.Id;
        }
    }
}
