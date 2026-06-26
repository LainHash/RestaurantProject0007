using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Services.Territory;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Persistence.Services.Territory
{
    public class RestaurantTableService : IRestaurantTableService
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        private readonly IMapper _mapper;
        public RestaurantTableService(IRestaurantTableRepository restaurantTableRepository, IMapper mapper)
        {
            _restaurantTableRepository = restaurantTableRepository;
            _mapper = mapper;
        }
        public async Task<DataResult<List<RestaurantTableResponse>>> 
            GetRestaurantTablesAsync(CancellationToken cancellationToken)
        {
            var tables = await _restaurantTableRepository.GetAllAsync(cancellationToken)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<RestaurantTableResponse>>(tables);
            return DataResult<List<RestaurantTableResponse>>
                .Success(response, Messages<RestaurantTable>.GetAllSuccess, HttpStatusCode.OK);
        }
    }
}
