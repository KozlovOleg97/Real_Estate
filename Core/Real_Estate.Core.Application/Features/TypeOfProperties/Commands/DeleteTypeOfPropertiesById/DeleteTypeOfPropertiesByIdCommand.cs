using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Commands.DeleteTypeOfPropertiesById
{
    public class DeleteTypeOfPropertiesByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTypeOfPropertiesByIdCommandHandler : 
        IRequestHandler<DeleteTypeOfPropertiesByIdCommand, int>
    {
        private readonly ITypeOfPropertiesRepository _typeOfPropertiesRepository;
        public DeleteTypeOfPropertiesByIdCommandHandler(
            ITypeOfPropertiesRepository typeOfPropertiesRepository)
        {
            _typeOfPropertiesRepository = typeOfPropertiesRepository;
        }
        public async Task<int> Handle(DeleteTypeOfPropertiesByIdCommand command, 
            CancellationToken cancellationToken)
        {
            var typeOfProperties = await _typeOfPropertiesRepository.GetByIdAsync(command.Id);

            if (typeOfProperties == null) 
                throw new Exception("Type of Property hasn't been found.");

            await _typeOfPropertiesRepository.DeleteAsync(typeOfProperties);

            return typeOfProperties.Id;
        }
    }
}
