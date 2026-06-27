using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Contracts.DTOs.Territory.Areas;

namespace Restaurant.Application.Services.Territory
{
    public interface IAreaService
    {
        Task<DataResult<IEnumerable<AreaResponse>>> 
            GetAreasAsync(GetAllAreaSpecification specification, CancellationToken cancellationToken);
    }
}
