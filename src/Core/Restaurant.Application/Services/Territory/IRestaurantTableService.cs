using Restaurant.Application.Common.Models.Result;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;

namespace Restaurant.Application.Services.Territory
{
    public interface IRestaurantTableService
    {
        Task<DataResult<List<RestaurantTableResponse>>> GetRestaurantTablesAsync(CancellationToken cancellationToken);
    }
}
