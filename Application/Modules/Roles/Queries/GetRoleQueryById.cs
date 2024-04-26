using Application.Responses;
using Application.Modules.Roles.Models;
using Application.Abstractions.MediatR;

namespace Application.Modules.Roles.Queries;

public record GetRoleQueryById : IQuery<DomainResponse<RoleDto>>
{
    public Guid Id { get; set; }
    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Id != Guid.Empty;
    public GetRoleQueryById(Guid id)
    {
        Id = id;
        IsValidCommand = CheckValidity();
    }
}