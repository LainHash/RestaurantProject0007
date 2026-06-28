using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Production;

namespace Restaurant.Application.Features.Production.Reservations.GetAllByWeek
{
    public record GetAllReservationByWeekQuery(DateTime WeekStart) : IQuery<DataResult<IEnumerable<ReservationResponse>>>;
}
