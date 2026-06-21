using System.Net;
using Restaurant.Application.Services;
using Restaurant.Application.Abstraction.Authentication;
using Restaurant.Application.Abstraction.Services;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Services.Auth;
using Restaurant.Contracts.DTOs.Authentication;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;

namespace Restaurant.Persistence.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonalInformationRepository _personalInfoRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPersonalInformationRepository personalInfoRepository,
            IPasswordHasher passwordHasher,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _personalInfoRepository = personalInfoRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                return Result.Fail("Email already exists.");
            }

            var customerRole = await _roleRepository.GetByNameAsync("Customer", cancellationToken);
            if (customerRole == null)
            {
                return Result.Fail("Default role Customer not found.");
            }

            var personalInfo = new PersonalInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DOB = request.DOB,
                Gender = request.Gender,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Phone = request.Phone,
                CitizenCardId = request.CitizenCardId,
                CreatedAt = DateTime.UtcNow
            };

            await _personalInfoRepository.AddAsync(personalInfo, cancellationToken);

            // Generate 6-digit verification code
            var random = new Random();
            var verificationCode = random.Next(100000, 999999).ToString();

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                IsActive = false,
                RoleId = customerRole.Id,
                PIId = personalInfo.Id,
                VerificationCode = verificationCode,
                VerificationCodeExpiresAt = DateTime.UtcNow.AddMinutes(15),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send activation email with the 6-digit code
            var subject = "Restaurant - Email Verification Code";
            var body = $"Hello {user.UserName},<br/><br/>Your verification code is: <b>{verificationCode}</b><br/>This code will expire in 15 minutes.";
            await _emailService.SendEmailAsync(user.Email, subject, body, cancellationToken);

            return Result.Success("User registered successfully. Please check your email for the verification code.");
        }

        public async Task<Result> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return Result.Fail("User not found.");
            }

            if (user.IsActive)
            {
                return Result.Fail("Account is already active.");
            }

            if (user.VerificationCode != request.Code)
            {
                return Result.Fail("Invalid verification code.");
            }

            if (user.VerificationCodeExpiresAt < DateTime.UtcNow)
            {
                return Result.Fail("Verification code has expired. Please request a new one.");
            }

            user.IsActive = true;
            user.VerificationCode = null;
            user.VerificationCodeExpiresAt = null;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Email verified successfully. You can now login.");
        }

        public async Task<DataResult<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return DataResult<AuthResponse>.Fail("Invalid email or password.", HttpStatusCode.Unauthorized);
            }

            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return DataResult<AuthResponse>.Fail("Invalid email or password.", HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                return DataResult<AuthResponse>.Fail("Account is not active. Please verify your email.", HttpStatusCode.Forbidden);
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
}
