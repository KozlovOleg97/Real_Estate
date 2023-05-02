using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.TypeOfSales;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Core.Application.Features.TypeOfSales.Queries.GetTypeOfSalesById
{
    public class GetTypeOfSalesByIdQuery : IRequest<TypeOfSalesViewModel>
    {
        [SwaggerParameter(Description = "The Id of the type of sale you want to consult")]
        public int Id { get; set; }
    }

    public class GetTypeOfSalesByIdQueryHandler : IRequestHandler<GetTypeOfSalesByIdQuery, 
        TypeOfSalesViewModel>
    {
        private readonly ITypeOfSalesRepository _typeOfSalesRepository;
        private readonly IMapper _mapper;

        public GetTypeOfSalesByIdQueryHandler(ITypeOfSalesRepository typeOfSalesRepository, 
            IMapper mapper)
        {
            _typeOfSalesRepository = typeOfSalesRepository;
            _mapper = mapper;
        }

        public async Task<TypeOfSalesViewModel> Handle(GetTypeOfSalesByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var TypeOfSale = await _typeOfSalesRepository.GetByIdAsync(query.Id);

            if (TypeOfSale is null) throw new Exception("Type of sale Not Found");

            var result = _mapper.Map<TypeOfSalesViewModel>(TypeOfSale);

            return result;
        }
    }
}
