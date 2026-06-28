using AutoMapper;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Commands.Create;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Production.Reservations.Queries.GetAllByWeek;
using Restaurant.Application.Services.Production;
using Restaurant.Contracts.DTOs.Production.Reservations;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Repositories.Production;
using System.Net;

namespace Restaurant.Persistence.Services.Production
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
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
            var resrevation = _mapper.Map<Reservation>(specification.RequestBody);

            if(resrevation.CustomerId is null)
            {
                resrevation.TemporaryContact = _mapper.Map<TemporaryContact>(specification.RequestBody);
            }

            await _reservationRepository.AddAsync(resrevation);
            await _reservationRepository.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ReservationResponse>(resrevation);
            return DataResult<ReservationResponse>
                .Success(response, Messages<Reservation>.AddSuccess, HttpStatusCode.Created);
        }
    }
}
