using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.Queries.GetAll
{
    public record GetAllReservationQuery : IQuery<DataResult<IEnumerable<ReservationResponse>>>
    {
    }
}
