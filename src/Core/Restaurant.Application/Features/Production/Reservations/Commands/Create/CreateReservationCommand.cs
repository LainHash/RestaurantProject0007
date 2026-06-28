using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public record CreateReservationCommand(CreateReservationRequest CreateReservationRequest) : ICommand<DataResult<ReservationResponse>>;
}
