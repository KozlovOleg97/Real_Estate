﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Commands.DeleteTypeOfPropertiesById
{
    public class DeleteTypeOfPropertiesByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "The ID of the type of property you want to delete")]
        public int Id { get; set; }
    }

    public class DeleteTypeOfPropertiesByIdCommandHandler : 
        IRequestHandler<DeleteTypeOfPropertiesByIdCommand, int>
    {
        private readonly ITypeOfPropertiesRepository _typeOfPropertiesRepository;
        private readonly IPropertiesRepository _propertiesRepository;
        public DeleteTypeOfPropertiesByIdCommandHandler(ITypeOfPropertiesRepository typeOfPropertiesRepository, 
            IPropertiesRepository propertiesRepository)
        {
            _typeOfPropertiesRepository = typeOfPropertiesRepository;
            _propertiesRepository = propertiesRepository;
        }

        public async Task<int> Handle(DeleteTypeOfPropertiesByIdCommand command, 
            CancellationToken cancellationToken)
        {
            var typeOfProperties = await _typeOfPropertiesRepository.GetByIdAsync(command.Id);

            if (typeOfProperties == null) 
                throw new Exception("Type of Property hasn't been found.");

            var properties = await _propertiesRepository.GetAllAsync();

            var propertiesRelational = properties.Where(x => 
                x.TypeOfPropertyId == command.Id).ToList();

            if (propertiesRelational.Count() != 0)
            {
                foreach (var property in propertiesRelational)
                {
                    await _propertiesRepository.DeleteAsync(property);
                }
            }

            await _typeOfPropertiesRepository.DeleteAsync(typeOfProperties);

            return typeOfProperties.Id;
        }
    }
}
