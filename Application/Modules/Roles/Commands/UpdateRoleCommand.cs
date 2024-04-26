using Application.Abstractions.MediatR;
using Application.Responses;
using MediatR;

namespace Application.Modules.Roles.Commands;
public record UpdateRoleCommand : ICommand<DomainResponse<Unit>>
{
    public bool IsValidCommand { get; private set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UpdateRoleCommand(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsValidCommand = CheckValidity();
    }
    private bool CheckValidity()
        => !string.IsNullOrEmpty(Name) && Id != Guid.Empty;
}
