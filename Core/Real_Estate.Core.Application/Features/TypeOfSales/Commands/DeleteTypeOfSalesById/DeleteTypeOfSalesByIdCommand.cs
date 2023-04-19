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

        public DeleteTypeOfSalesByIdCommandHandler(ITypeOfSalesRepository typeOfSalesRepository)
        {
            _typeOfSalesRepository = typeOfSalesRepository;
        }

        public async Task<int> Handle(DeleteTypeOfSalesByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSales = await _typeOfSalesRepository.GetByIdAsync(command.Id);

            if (typeOfSales == null)
                throw new Exception("Type of Sale hasn't been found.");

            await _typeOfSalesRepository.DeleteAsync(typeOfSales);

            return typeOfSales.Id;
        }
    }
}
