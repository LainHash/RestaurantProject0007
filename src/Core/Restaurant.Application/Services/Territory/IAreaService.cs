using Restaurant.Application.Common.Models.Result;
using Restaurant.Contracts.DTOs.Territory.Areas;

namespace Restaurant.Application.Services.Territory
{
    public interface IAreaService
    {
        Task<DataResult<List<AreaResponse>>> GetAreasAsync(CancellationToken cancellationToken); 
    }
}
