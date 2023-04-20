using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;

namespace Real_Estate.Core.Application.Features.TypeOfSales.Commands.DeleteTypeOfSalesById
{
    public class DeleteTypeOfSalesByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTypeOfSalesByIdCommandHandler : IRequestHandler<DeleteTypeOfSalesByIdCommand, int>
    {
        private readonly ITypeOfSalesRepository _typeOfSalesRepository;
        private readonly IPropertiesRepository _propertiesRepository;

        public DeleteTypeOfSalesByIdCommandHandler(ITypeOfSalesRepository typeOfSalesRepository, 
            IPropertiesRepository propertiesRepository)
        {
            _typeOfSalesRepository = typeOfSalesRepository;
            _propertiesRepository = propertiesRepository;
        }

        public async Task<int> Handle(DeleteTypeOfSalesByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSales = await _typeOfSalesRepository.GetByIdAsync(command.Id);

            if (typeOfSales == null)
                throw new Exception("Type of Sale hasn't been found.");

            var properties = await _propertiesRepository.GetAllAsync();

            var propertiesRelational = properties.Where(x => x.ImprovementsId == command.Id).ToList();

            if (propertiesRelational.Count() != 0)
            {
                foreach (var property in propertiesRelational)
                {
                    await _propertiesRepository.DeleteAsync(property);
                }
            }

            await _typeOfSalesRepository.DeleteAsync(typeOfSales);

            return typeOfSales.Id;
        }
    }
}
