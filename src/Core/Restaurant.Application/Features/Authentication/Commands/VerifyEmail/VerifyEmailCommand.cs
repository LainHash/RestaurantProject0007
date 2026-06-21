using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Features.Authentication.Commands.VerifyEmail;

public class VerifyEmailCommand : ICommand<Result>
{
    public VerifyEmailRequest Request { get; set; }

    public VerifyEmailCommand(VerifyEmailRequest request)
    {
        Request = request;
    }
}
