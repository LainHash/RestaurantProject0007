using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Reservations.GetAll
{
    public class GetAllReservationSpecification : BaseSpecification<Reservation>
    {
        public GetAllReservationSpecification(GetAllReservationQuery request)
        {
            AddIgnoreQueryFilters();
        }
    }
}
