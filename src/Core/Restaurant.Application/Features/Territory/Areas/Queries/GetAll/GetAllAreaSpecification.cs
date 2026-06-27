using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public class GetAllAreaSpecification : BaseSpecification<Area>
    {
        public GetAllAreaSpecification(GetAllAreaQuery request)
        {
            AddInclude(a => a.RestaurantTables);

            bool hasAreaType = !string.IsNullOrWhiteSpace(request.AreaType);

            if (hasAreaType)
            {
                Criteria = a => a.Type.ToLower()
                .Contains(request.AreaType!.ToLower());
            }
        }
    }
}
