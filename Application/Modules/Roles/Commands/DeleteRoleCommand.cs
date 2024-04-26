using MediatR;
using Application.Responses;
using Application.Abstractions.MediatR;

namespace Application.Modules.Roles.Commands;

public record DeleteRoleCommand: ICommand<DomainResponse<Unit>>
{
    public Guid Id { get; set; }
    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Id != Guid.Empty;
    public DeleteRoleCommand(Guid id)
    {
        Id = id;
        IsValidCommand = CheckValidity();
    } 
}
