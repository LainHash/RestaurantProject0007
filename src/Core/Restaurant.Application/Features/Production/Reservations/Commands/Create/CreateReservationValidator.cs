using FluentValidation;

namespace Restaurant.Application.Features.Production.Reservations.Commands.Create
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            // Kiểm tra UserId (người dùng đã đăng nhập có token)
            When(x => x.UserId.HasValue, () =>
            {
                RuleFor(x => x.CreateReservationRequest.GuestName)
                    .Empty().WithMessage("Guest name must be empty when user is authenticated.");

                RuleFor(x => x.CreateReservationRequest.GuestEmail)
                    .Empty().WithMessage("Guest email must be empty when user is authenticated.");

                RuleFor(x => x.CreateReservationRequest.GuestPhone)
                    .Empty().WithMessage("Guest phone must be empty when user is authenticated.");
            });
            // Kiểm tra khi không có UserId (khách vãng lai, không có token)
            When(x => !x.UserId.HasValue, () =>
            {
                RuleFor(x => x.CreateReservationRequest.GuestName)
                    .NotEmpty().WithMessage("Guest name is required when user is not authenticated.");

                RuleFor(x => x.CreateReservationRequest.GuestEmail)
                    .NotEmpty().WithMessage("Guest email is required when user is not authenticated.");

                RuleFor(x => x.CreateReservationRequest.GuestPhone)
                    .NotEmpty().WithMessage("Guest phone is required when user is not authenticated.");
            });
        }
    }
}
