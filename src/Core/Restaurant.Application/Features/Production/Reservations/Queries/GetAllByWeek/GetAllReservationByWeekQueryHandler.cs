using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Production;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek
{
    public class GetAllReservationByWeekQueryHandler : IQueryHandler<GetAllReservationByWeekQuery, DataResult<IEnumerable<ReservationResponse>>>
    {
        private readonly IReservationService _reservationService;
        public GetAllReservationByWeekQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<DataResult<IEnumerable<ReservationResponse>>> Handle(GetAllReservationByWeekQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllReservationByWeekSpecification(request);
            var response = await _reservationService.GetReservationByWeekAsync(specification, cancellationToken);
            return response;
        }
    }
}
