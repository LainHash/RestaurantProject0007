using FluentValidation;

namespace Restaurant.Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Request.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Request.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Request.Password).WithMessage("Confirm Password must match the Password.");

        RuleFor(x => x.Request.FirstName)
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(x => x.Request.LastName)
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(x => x.Request.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10,15}$").WithMessage("Phone number must be valid.");

        RuleFor(x => x.Request.CitizenCardId)
            .NotEmpty().WithMessage("Citizen Card ID is required.");
    }
}
