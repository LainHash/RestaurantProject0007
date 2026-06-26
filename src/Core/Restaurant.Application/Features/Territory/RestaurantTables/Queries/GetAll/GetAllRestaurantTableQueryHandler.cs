using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Territory;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll
{
    public class GetAllRestaurantTableQueryHandler : IQueryHandler<GetAllRestaurantTableQuery, DataResult<List<RestaurantTableResponse>>>
    {
        private readonly IRestaurantTableService _restaurantTableService;
        public GetAllRestaurantTableQueryHandler(IRestaurantTableService restaurantTableService)
        {
            _restaurantTableService = restaurantTableService;
        }
        public async Task<DataResult<List<RestaurantTableResponse>>> Handle(GetAllRestaurantTableQuery request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableService.GetRestaurantTablesAsync(cancellationToken);
            return response;
        }
    }
}
