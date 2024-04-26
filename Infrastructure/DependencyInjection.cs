using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Abstractions.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Services;
using Infrastructure.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Persistances here
        services
        .AddDistributedMemoryCache()
        .AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(
                   name:"DefaultConnection"));
        })
        .AddScoped<IPersistanceService<Role>, PersistanceService<Role, ApplicationDbContext>>()
        .AddScoped<IPersistanceService<User>, PersistanceService<User, ApplicationDbContext>>();

        //API Version here
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true;
        });

        //Authentication and Authorization here
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("Jwt:Issuer").Get<string>(),
                    ValidAudience = configuration.GetSection("Jwt:Audience").Get<string>(),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration?.GetSection("Jwt:SecurityKey")?.Get<string>() ?? 
                            throw new ArgumentNullException("Jwt:SecurityKey cant be null")))
                };
            });

        return services;
    }
}