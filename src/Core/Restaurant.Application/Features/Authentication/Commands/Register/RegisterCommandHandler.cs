using Restaurant.Application.Abstraction.Authentication;
using Restaurant.Application.Abstraction.Services;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;

namespace Restaurant.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Result.Fail("Email already exists.");
        }

        var customerRole = await _roleRepository.GetByNameAsync("Customer", cancellationToken);
        if (customerRole == null)
        {
            return Result.Fail("Default role Customer not found.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.Request.UserName,
            Email = request.Request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Request.Password),
            IsActive = false,
            RoleId = customerRole.Id,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send activation email
        var subject = "Welcome to Restaurant! Please activate your account.";
        var body = $"Hello {user.UserName},<br/><br/>Welcome to our restaurant! Your account has been created successfully.";
        await _emailService.SendEmailAsync(user.Email, subject, body, cancellationToken);

        return Result.Success("User registered successfully. Please check your email to activate your account.");
    }
}
