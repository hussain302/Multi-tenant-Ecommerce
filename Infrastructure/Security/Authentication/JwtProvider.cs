using System.Text;
using Domain.Entities;
using System.Security.Claims;
using Application.Abstractions.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Security.Authentication;
public sealed class JwtProvider(
    IOptions<JwtOptions> jwtOptions) 
    : IJwtProvider
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;

    public string GenerateToken(User user)
    {
        Claim[]? claims =
        [
            new (JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new (JwtRegisteredClaimNames.NameId, user.Username),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Name, $"{user.FirstName}, {user.LastName}")
        ];

        SigningCredentials sigingCredentials = new(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(jwtOptions.Issuer,
            jwtOptions.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(90),
              signingCredentials: sigingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
