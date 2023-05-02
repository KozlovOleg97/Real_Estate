using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.ViewModels.TypeOfProperties;

namespace Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetAllTypeOfProperties
{
    public class GetAllTypeOfPropertiesQuery : IRequest<IEnumerable<TypeOfPropertiesViewModel>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllTypeOfPropertiesQuery, 
            IEnumerable<TypeOfPropertiesViewModel>>
        {

            private readonly ITypeOfPropertiesRepository _TypeOfPropertiesRepository;
            private readonly IMapper _mapper;
            public GetAllCategoriesQueryHandler(ITypeOfPropertiesRepository TypeOfPropertiesRepository, 
                IMapper mapper)
            {
                _TypeOfPropertiesRepository = TypeOfPropertiesRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<TypeOfPropertiesViewModel>> Handle(
                GetAllTypeOfPropertiesQuery request, CancellationToken cancellationToken)
            {
                var typeOfPropertiesViewModel = await GetAllViewModel();

                return typeOfPropertiesViewModel;
            }

            private async Task<List<TypeOfPropertiesViewModel>> GetAllViewModel()
            {
                var typeOfPropertiesList = await _TypeOfPropertiesRepository.GetAllAsync();

                if (typeOfPropertiesList.Count() == 0) 
                    throw new Exception("There are not TypeOfProperties.");

                var result = _mapper.Map<List<TypeOfPropertiesViewModel>>(typeOfPropertiesList);

                return result;
            }
        }
    }
}
