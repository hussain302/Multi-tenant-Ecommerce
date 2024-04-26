using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Security.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Infrastructure.OptionsSetup;

public class JwtBearerOptionsSetup(IOptions<JwtOptions> options) : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions jwtOptions = options.Value;
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
        };
    }
}
