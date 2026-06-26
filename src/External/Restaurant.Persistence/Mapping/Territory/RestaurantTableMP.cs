using AutoMapper;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Persistence.Mapping.Territory
{
    public class RestaurantTableMP : Profile
    {
        public RestaurantTableMP()
        {
            CreateMap<RestaurantTable, RestaurantTableResponse>();
        }
    }
}
