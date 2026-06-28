using Restaurant.Contracts.DTOs.Production.Reservations;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public class CreateReservationSpecification : BaseSpecification<Reservation>
    {
        public Guid? UserId { get; set; }
        public CreateReservationRequest RequestBody { get; set; }
        public CreateReservationSpecification(CreateReservationCommand request)
        {
            RequestBody = request.CreateReservationRequest;
            UserId = request.UserId;
        }
    }
}
