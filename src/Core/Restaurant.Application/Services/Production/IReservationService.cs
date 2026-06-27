using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Production.Reservations;
using Restaurant.Contracts.DTOs.Production;

namespace Restaurant.Application.Services.Production
{
    public interface IReservationService
    {
        Task<DataResult<IEnumerable<ReservationResponse>>> 
            GetReservationsAsync(GetAllReservationSpecification specification,CancellationToken cancellationToken);
    }
}
