using Application.Responses;
using Application.Modules.Auth.Models;
using Application.Abstractions.MediatR;

namespace Application.Modules.Auth.Commands;
public record LoginCommand : ICommand<DomainResponse<LoginResponseDto>>
{
    public string? Username { get; private set; }
    public string? Email { get; private set; }
    public string Password { get; private set; }
    public bool IsValidCommand { get; private set; }

    public LoginCommand(
        string? username,
        string? email,
        string password)
    {
        Username = username;
        Email = email;
        Password = password;
        IsValidCommand = CheckValidity();
    }

    private bool CheckValidity()
     => (!string.IsNullOrEmpty(Username)
     || !string.IsNullOrEmpty(Email))
     && !string.IsNullOrEmpty(Password);
}