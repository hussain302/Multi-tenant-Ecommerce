using Application.Modules.Auth.Models;
using Domain.Entities;

namespace Application.Services.Mapping;
public static class AuthMapper
{
    public static LoginResponseDto AsLoginResponseDto(
        this User source,
        string accessToken)
        => new  
            (
                source.Id,
                source.Username,
                source.FirstName,
                source.IsActive,
                source.LastName,
                source.Email,
                accessToken
            );
}
