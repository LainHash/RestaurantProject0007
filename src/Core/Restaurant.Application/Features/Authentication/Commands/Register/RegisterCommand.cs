using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : ICommand<Result>
{
    public RegisterRequest Request { get; set; }

    public RegisterCommand(RegisterRequest request)
    {
        Request = request;
    }
}
