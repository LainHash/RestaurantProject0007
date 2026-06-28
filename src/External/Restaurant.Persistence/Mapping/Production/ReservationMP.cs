using AutoMapper;
using Restaurant.Contracts.DTOs.Guests.Customers;
using Restaurant.Contracts.DTOs.Misc;
using Restaurant.Contracts.DTOs.Production.Reservations;
using Restaurant.Domain.Entities.Guests;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Persistence.Mapping.Production
{
    public class ReservationMP : Profile
    {
        public ReservationMP()
        {
            CreateMap<Reservation, ReservationResponse>();
            CreateMap<CreateReservationRequest, Reservation>();
            CreateMap<CreateReservationRequest, TemporaryContact>();
            CreateMap<TemporaryContact, TemporaryContactResponse>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
