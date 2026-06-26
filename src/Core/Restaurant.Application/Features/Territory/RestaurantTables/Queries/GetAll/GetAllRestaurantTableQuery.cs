using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll
{
    public record GetAllRestaurantTableQuery : IQuery<DataResult<List<RestaurantTableResponse>>>;
}
