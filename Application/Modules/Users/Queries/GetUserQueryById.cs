using Application.Responses;
using Application.Abstractions.MediatR;
using Application.Modules.Users.Models;

namespace Application.Modules.Users.Queries;

public record GetUserQueryById : IQuery<DomainResponse<UserDto>>
{
    public Guid Id { get; set; }
    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Id != Guid.Empty;
    public GetUserQueryById(Guid id)
    {
        Id = id;
        IsValidCommand = CheckValidity();
    }
}