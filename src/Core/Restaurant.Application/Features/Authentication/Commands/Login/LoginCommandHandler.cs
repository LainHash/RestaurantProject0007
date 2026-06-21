using System.Net;
using Restaurant.Application.Abstraction.Authentication;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Authentication;
using Restaurant.Domain.Repositories.Identity;

namespace Restaurant.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, DataResult<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<DataResult<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
        if (user == null)
        {
            return DataResult<AuthResponse>.Fail("Invalid email or password.", HttpStatusCode.Unauthorized);
        }

        if (!_passwordHasher.VerifyPassword(request.Request.Password, user.PasswordHash))
        {
            return DataResult<AuthResponse>.Fail("Invalid email or password.", HttpStatusCode.Unauthorized);
        }

        if (!user.IsActive)
        {
            return DataResult<AuthResponse>.Fail("Account is not active.", HttpStatusCode.Forbidden);
        }

        var role = await _roleRepository.GetByIdAsync(user.RoleId, cancellationToken);
        var roleName = role?.Name ?? "Customer";

        var token = _jwtProvider.GenerateToken(user.Id, user.UserName, user.Email, roleName);

        var response = new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = token
        };

        return DataResult<AuthResponse>.Success(response, "Login successful.");
    }
}
