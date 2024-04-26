using MediatR;
using Domain.Entities;
using Application.Responses;
using Microsoft.Data.SqlClient;
using Application.Services.Mapping;
using Application.Abstractions.Jwt;
using Application.Modules.Auth.Models;
using Application.Abstractions.Services;
using Application.Modules.Auth.Commands;
using Application.Services.Cryptography;

namespace Application.Modules.Auth.Handlers;

public class AuthCommandHandlers(
    IPersistanceService<User> persistance,
    IJwtProvider jwtProvider) :
    IRequestHandler<LoginCommand, DomainResponse<LoginResponseDto>>
{
    [Obsolete]
    public async Task<DomainResponse<LoginResponseDto>> Handle(
            LoginCommand command,
            CancellationToken cancellationToken)
    {
        try
        {
            string token = string.Empty;
            bool isLoginSuccessful;
            LoginResponseDto? loginResponseDto = default;
            List<SqlParameter> parameters =
            [
                new SqlParameter("@Username", command.Username),
                new SqlParameter("@Email", command.Email)
            ];

            User user = (User)await persistance.ExecuteNonCollectionQueryAsync(
                sql: "uspLoginUser",
                parameters: [.. parameters],
                cancellationToken: cancellationToken);

            if (user is not null)
            {
                isLoginSuccessful = PasswordHasher.VerifyPassword(command.Password, user.PasswordHash);
                if (isLoginSuccessful) token = jwtProvider.GenerateToken(user ?? throw new ArgumentNullException(nameof(user)));
                loginResponseDto = user?.AsLoginResponseDto(token) ?? default;
                return new DomainResponse<LoginResponseDto>
                {
                    Success = isLoginSuccessful,
                    Message = isLoginSuccessful ?
                              "User has been successfully logged in." :
                              "Incorrect username or password.",
                    Data = loginResponseDto
                };
            }
            else
            {
                return new DomainResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = default
                };
            }
        }
        catch (Exception ex)
        {
            return new DomainResponse<LoginResponseDto>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = default
            };
        }
    }
}