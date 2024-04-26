using Domain.Entities;

namespace Application.Abstractions.Jwt;
public interface IJwtProvider
{
    string GenerateToken(User user);
}
