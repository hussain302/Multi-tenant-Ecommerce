using MediatR;
using Application.Responses;
using Application.Abstractions.MediatR;

namespace Application.Modules.Users.Commands;

public record DeleteUserCommand: ICommand<DomainResponse<Unit>>
{
    public Guid Id { get; set; }
    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Id != Guid.Empty;
    public DeleteUserCommand(Guid id)
    {
        Id = id;
        IsValidCommand = CheckValidity();
    } 
}
