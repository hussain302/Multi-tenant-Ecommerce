using Application.Abstractions.MediatR;
using Application.Responses;

namespace Application.Modules.Roles.Commands;
public record CreateRoleCommand : ICommand<DomainResponse<Guid>>
{
    public bool IsValidCommand { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CreateRoleCommand(string name, string description)
    {
        Name = name;
        Description = description;
        IsValidCommand = CheckValidity();
    }
    private bool CheckValidity()
        => !string.IsNullOrEmpty(Name) 
        && !string.IsNullOrEmpty(Description);
}
