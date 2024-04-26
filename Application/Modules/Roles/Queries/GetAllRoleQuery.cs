using Application.Responses;
using Application.Modules.Roles.Models;
using Application.Abstractions.MediatR;

namespace Application.Modules.Roles.Queries;

public record GetAllRoleQuery
    : IQuery<DomainResponse<PaginatedResult<List<RoleDto>>>>
{
    public int Start { get; set; }
    public int Limit { get; set; }
    public string? OrderBy { get; set; }
    public string? Order { get; set; }

    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Start > -1 && Limit > 1;
    public GetAllRoleQuery(int start,
        int limit,
        string? orderBy,
        string? order)
    {
        Start = start;
        Limit = limit;
        OrderBy = orderBy;
        Order = order;
        IsValidCommand = CheckValidity();
    }
}