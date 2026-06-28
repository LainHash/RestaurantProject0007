using FluentValidation;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            When(x => x.CreateReservationRequest.CustomerId.HasValue, () =>
            {
                RuleFor(x => x.CreateReservationRequest.GuestName)
                    .Empty().WithMessage("Guest name must be empty when Customer Id is provided.");
                
                RuleFor(x => x.CreateReservationRequest.GuestEmail)
                    .Empty().WithMessage("Guest email must be empty when Customer Id is provided.");
                
                RuleFor(x => x.CreateReservationRequest.GuestPhone)
                    .Empty().WithMessage("Guest phone must be empty when Customer Id is provided.");
            });

            When(x => !x.CreateReservationRequest.CustomerId.HasValue, () =>
            {
                RuleFor(x => x.CreateReservationRequest.GuestName)
                    .NotEmpty().WithMessage("Guest name is required when Customer Id is not provided.");
                
                RuleFor(x => x.CreateReservationRequest.GuestEmail)
                    .NotEmpty().WithMessage("Guest email is required when Customer Id is not provided.");
                
                RuleFor(x => x.CreateReservationRequest.GuestPhone)
                    .NotEmpty().WithMessage("Guest phone is required when Customer Id is not provided.");
            });
        }
    }
}
