using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Production;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.GetAll
{
    public class GetAllReservationQueryHandler : IQueryHandler<GetAllReservationQuery, DataResult<IEnumerable<ReservationResponse>>>
    {
        private readonly IReservationService _reservationService;
        public GetAllReservationQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<DataResult<IEnumerable<ReservationResponse>>> Handle(GetAllReservationQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllReservationSpecification(request);
            var response = await _reservationService.GetReservationsAsync(specification, cancellationToken);
            return response;
        }
    }
}
