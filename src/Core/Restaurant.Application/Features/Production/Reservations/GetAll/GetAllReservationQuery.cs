using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Production;

namespace Restaurant.Application.Features.Production.Reservations.GetAll
{
    public record GetAllReservationQuery : IQuery<DataResult<IEnumerable<ReservationResponse>>>
    {
    }
}
