using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Production;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, DataResult<ReservationResponse>>
    {
        private readonly IReservationService _reservationService;
        public CreateReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<DataResult<ReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateReservationSpecification(request);
            var response = await _reservationService.CreateReservationAsync(specification, cancellationToken);
            return response;
        }
    }
}
