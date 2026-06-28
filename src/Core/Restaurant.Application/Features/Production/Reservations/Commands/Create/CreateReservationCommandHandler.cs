using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, DataResult<ReservationResponse>>
    {
        public Task<DataResult<ReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
