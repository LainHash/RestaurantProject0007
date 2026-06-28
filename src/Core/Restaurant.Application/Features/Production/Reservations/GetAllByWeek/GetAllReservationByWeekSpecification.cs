using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Reservations.GetAllByWeek
{
    public class GetAllReservationByWeekSpecification : BaseSpecification<Reservation>
    {
        public GetAllReservationByWeekSpecification(GetAllReservationByWeekQuery request)
        {
            AddInclude(r => r.Customer!);
            AddInclude(r => r.TemporaryContact!);
            AddInclude(r => r.RestaurantTable);

            var startOfWeek = request.WeekStart.Date;
            var endOfWeek = startOfWeek.AddDays(7);

            Criteria = r => r.ReservationTime >= startOfWeek && r.ReservationTime < endOfWeek;
        }
    }
}
