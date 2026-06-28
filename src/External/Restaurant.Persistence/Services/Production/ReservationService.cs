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
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Contracts.Common.Enums;

namespace Restaurant.Persistence.Services.Production
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository,ICustomerRepository customerRepository, IRestaurantTableRepository restaurantTableRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _restaurantTableRepository = restaurantTableRepository;
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
            var requestBody = specification.RequestBody;
            var reservation = _mapper.Map<Reservation>(requestBody);

            if (!requestBody.RestaurantTableId.HasValue || requestBody.RestaurantTableId == Guid.Empty)
            {
                var tablesQuery = _restaurantTableRepository.GetAllAsync(cancellationToken)
                    .Include(t => t.Area)
                    .Where(t => t.Capacity >= requestBody.NumberOfGuests);

                if (!string.IsNullOrEmpty(requestBody.TableType))
                {
                    tablesQuery = tablesQuery.Where(t => t.Area.Type.ToLower() == requestBody.TableType.ToLower());
                }

                // Check for overlapping reservations (+/- 2 hours)
                var startTime = requestBody.ReservationTime.AddHours(-2);
                var endTime = requestBody.ReservationTime.AddHours(2);

                var reservedTableIds = await _reservationRepository.GetAllAsync(cancellationToken)
                    .Where(r => r.ReservationTime > startTime && r.ReservationTime < endTime && r.Status != nameof(ReservationStatus.Cancelled))
                    .Select(r => r.RestaurantTableId)
                    .ToListAsync(cancellationToken);

                var availableTable = await tablesQuery
                    .Where(t => !reservedTableIds.Contains(t.Id) && (t.Status != nameof(TableStatus.Maintenance) || t.Status != nameof(TableStatus.Reserved)))
                    .OrderBy(t => t.Capacity)
                    .FirstOrDefaultAsync(cancellationToken);

                if (availableTable == null)
                {
                    return DataResult<ReservationResponse>
                        .Fail("No available table found for the specified time and guests.", HttpStatusCode.BadRequest);
                }

                reservation.RestaurantTableId = availableTable.Id;
            }
            else
            {
                reservation.RestaurantTableId = requestBody.RestaurantTableId.Value;
            }

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
                reservation.TemporaryContact = _mapper.Map<TemporaryContact>(requestBody);
            }

            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<ReservationResponse>(reservation);
            return DataResult<ReservationResponse>.Success(response, Messages<Reservation>.AddSuccess, HttpStatusCode.Created);
        }
    }
}
