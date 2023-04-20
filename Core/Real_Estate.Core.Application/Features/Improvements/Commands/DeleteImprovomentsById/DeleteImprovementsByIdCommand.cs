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

        public DeleteImprovementsByIdCommandHandler(IImprovementsRepository improvementsRepository, 
            IPropertiesRepository propertiesRepository)
        {
            _improvementsRepository = improvementsRepository;
            _propertiesRepository = propertiesRepository;
        }

        public async Task<int> Handle(DeleteImprovementsByIdCommand command, 
            CancellationToken cancellationToken)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(command.Id);

            if (improvements == null)
                throw new Exception("Improvements Not Found.");

            var properties = await _propertiesRepository.GetAllAsync();

            // change Improvements.Id on TypeOfPropertyId
            var propertiesRelational = properties.Where(x => 
                x.TypeOfPropertyId == command.Id).ToList();

            if (propertiesRelational.Count() != 0)
            {
                foreach (var property in propertiesRelational)
                {
                    await _propertiesRepository.DeleteAsync(property);
                }
            }

            await _improvementsRepository.DeleteAsync(improvements);

            return improvements.Id;
        }
    }
}
