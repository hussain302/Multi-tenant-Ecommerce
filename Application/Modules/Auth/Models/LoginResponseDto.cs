using Application.Modules.Roles.Models;

namespace Application.Modules.Auth.Models;

public record LoginResponseDto(
    Guid Id, 
    string Username,
    string FirstName, 
    bool IsActive,
    string LastName,
    string Email, 
    string AccessToken);