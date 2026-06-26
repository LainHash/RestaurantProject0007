using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Features.Authentication.Commands.CompleteProfile;

public class CompleteProfileCommand : ICommand<Result>
{
    public CompleteProfileRequest Request { get; set; }

    public CompleteProfileCommand(CompleteProfileRequest request)
    {
        Request = request;
    }
}
