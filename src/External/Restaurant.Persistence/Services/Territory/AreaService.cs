using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
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

        public async Task<DataResult<List<AreaResponse>>> 
            GetAreasAsync(CancellationToken cancellationToken)
        {
            var categories = _areaRepository.GetAllAsync(cancellationToken)
                .ToListAsync();

            var response = _mapper.Map<List<AreaResponse>>(categories);
            return DataResult<List<AreaResponse>>
                .Success(response, Messages<Area>.GetAllSuccess, HttpStatusCode.OK);
        }
    }
}
