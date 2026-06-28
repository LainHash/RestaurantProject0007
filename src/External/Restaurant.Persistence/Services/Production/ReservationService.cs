using AutoMapper;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek;
using Restaurant.Application.Services.Production;
using Restaurant.Contracts.DTOs.Production.Reservations;
using Restaurant.Domain.Entities.Guests;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Repositories.Customers;
using Restaurant.Domain.Repositories.Production;
using System.Net;

namespace Restaurant.Persistence.Services.Production
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository,ICustomerRepository customerRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<DataResult<IEnumerable<ReservationResponse>>>
            GetReservationsAsync(GetAllReservationSpecification specification, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);
            return DataResult<IEnumerable<ReservationResponse>>
                .Success(response, Messages<Reservation>.GetAllSuccess, HttpStatusCode.OK);
        }

        public async Task<DataResult<IEnumerable<ReservationResponse>>>
            GetReservationByWeekAsync(GetAllReservationByWeekSpecification specification, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);
            return DataResult<IEnumerable<ReservationResponse>>
                .Success(response, Messages<Reservation>.GetAllSuccess, HttpStatusCode.OK);
        }

        public async Task<DataResult<ReservationResponse>> 
            CreateReservationAsync(CreateReservationSpecification specification, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(specification.RequestBody);

            if(specification.UserId.HasValue)
            {
                var customer = await _customerRepository.GetByUserIdAsync(specification.UserId.Value, cancellationToken);

                if (customer is not null)
                {
                    reservation.CustomerId = customer.Id;
                }
            }
            else
            {
                reservation.TemporaryContact = _mapper.Map<TemporaryContact>(specification.RequestBody);
            }

            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<ReservationResponse>(reservation);
            return DataResult<ReservationResponse>.Success(response, Messages<Reservation>.AddSuccess, HttpStatusCode.Created);
        }
    }
}
