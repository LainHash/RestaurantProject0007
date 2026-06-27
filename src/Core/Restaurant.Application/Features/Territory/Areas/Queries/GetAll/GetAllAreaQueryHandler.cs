using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Territory;
using Restaurant.Contracts.DTOs.Territory.Areas;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public class GetAllAreaQueryHandler : IQueryHandler<GetAllAreaQuery, DataResult<IEnumerable<AreaResponse>>>
    {
        private readonly IAreaService _areaService;
        public GetAllAreaQueryHandler(IAreaService areaService)
        {
            _areaService = areaService;
        }

        public async Task<DataResult<IEnumerable<AreaResponse>>> Handle(GetAllAreaQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllAreaSpecification(request);
            var response = await _areaService.GetAreasAsync(specification, cancellationToken);
            return response;
        }
    }
}
