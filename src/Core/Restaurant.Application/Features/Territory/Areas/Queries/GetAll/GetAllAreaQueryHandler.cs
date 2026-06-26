using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Territory;
using Restaurant.Contracts.DTOs.Territory.Areas;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public class GetAllAreaQueryHandler : IQueryHandler<GetAllAreaQuery, DataResult<List<AreaResponse>>>
    {
        private readonly IAreaService _areaService;
        public GetAllAreaQueryHandler(IAreaService areaService)
        {
            _areaService = areaService;
        }

        public async Task<DataResult<List<AreaResponse>>> Handle(GetAllAreaQuery request, CancellationToken cancellationToken)
        {
            var response = await _areaService.GetAreasAsync(cancellationToken);
            return response;
        }
    }
}
