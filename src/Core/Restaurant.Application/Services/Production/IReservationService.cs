using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Production.Reservations.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek;
using Restaurant.Contracts.DTOs.Production.Reservations;

namespace Restaurant.Application.Services.Production
{
    public interface IReservationService
    {
        Task<DataResult<IEnumerable<ReservationResponse>>> 
            GetReservationsAsync(GetAllReservationSpecification specification,CancellationToken cancellationToken);

        Task<DataResult<IEnumerable<ReservationResponse>>>
            GetReservationByWeekAsync(GetAllReservationByWeekSpecification specification, CancellationToken cancellationToken);

        Task<DataResult<ReservationResponse>>
            CreateReservationAsync(CreateReservationSpecification specification, CancellationToken cancellationToken);
    }
}
