using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.TypeOfSales;

namespace Real_Estate.Core.Application.Features.TypeOfSales.Queries.GetTypeOfSalesById
{
    public class GetTypeOfSalesByIdQuery : IRequest<TypeOfSalesViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTypeOfSalesByIdQueryHandler : IRequestHandler<GetTypeOfSalesByIdQuery, 
        TypeOfSalesViewModel>
    {
        private readonly ITypeOfSalesRepository _TypeOfSalesRepository;
        private readonly IMapper _mapper;

        public GetTypeOfSalesByIdQueryHandler(ITypeOfSalesRepository TypeOfSalesRepository, 
            IMapper mapper)
        {
            _TypeOfSalesRepository = TypeOfSalesRepository;
            _mapper = mapper;
        }

        public async Task<TypeOfSalesViewModel> Handle(GetTypeOfSalesByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var TypeOfSale = await _TypeOfSalesRepository.GetByIdAsync(query.Id);

            if (TypeOfSale is null) throw new Exception("Type of sale Not Found");

            var result = _mapper.Map<TypeOfSalesViewModel>(TypeOfSale);

            return result;
        }
    }
}
