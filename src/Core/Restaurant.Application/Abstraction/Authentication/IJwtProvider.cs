namespace Restaurant.Application.Abstraction.Authentication;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string userName, string email, string role);
}
