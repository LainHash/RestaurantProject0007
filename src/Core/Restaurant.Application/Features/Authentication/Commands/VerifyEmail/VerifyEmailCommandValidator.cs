using FluentValidation;

namespace Restaurant.Application.Features.Authentication.Commands.VerifyEmail;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Request.Code)
            .NotEmpty().WithMessage("Verification Code is required.")
            .Length(6).WithMessage("Verification Code must be exactly 6 digits.");
    }
}
