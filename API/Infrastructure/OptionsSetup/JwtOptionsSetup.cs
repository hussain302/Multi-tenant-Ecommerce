using Microsoft.Extensions.Options;
using Infrastructure.Security.Authentication;

namespace API.Infrastructure.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{

    private const string SectioName = "Jwt";
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectioName).Bind(options);
    }
}
