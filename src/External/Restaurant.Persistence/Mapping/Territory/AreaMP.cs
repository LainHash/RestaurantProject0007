using AutoMapper;
using Restaurant.Contracts.DTOs.Territory.Areas;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Persistence.Mapping.Territory
{
    public class AreaMP : Profile
    {
        public AreaMP()
        {
            CreateMap<Area, AreaResponse>();
        }
    }
}
