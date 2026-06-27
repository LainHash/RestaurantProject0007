using AutoMapper;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Application.Services.Territory;
using Restaurant.Contracts.DTOs.Territory.Areas;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Persistence.Services.Territory
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository areaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<DataResult<IEnumerable<AreaResponse>>>
            GetAreasAsync(GetAllAreaSpecification specification, CancellationToken cancellationToken)
        {
            var areas = await _areaRepository.GetAllAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<AreaResponse>>(areas);

            return DataResult<IEnumerable<AreaResponse>>
                .Success(response, Messages<Area>.GetAllSuccess, HttpStatusCode.OK);
        }
    }
}
