using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Production.Reservations.GetAll;
using Restaurant.Application.Features.Production.Reservations.GetAllByWeek;
using Restaurant.Contracts.DTOs.Production;

namespace Restaurant.Application.Services.Production
{
    public interface IReservationService
    {
        Task<DataResult<IEnumerable<ReservationResponse>>> 
            GetReservationsAsync(GetAllReservationSpecification specification,CancellationToken cancellationToken);

        Task<DataResult<IEnumerable<ReservationResponse>>>
            GetReservationByWeekAsync(GetAllReservationByWeekSpecification specification, CancellationToken cancellationToken);
    }
}
