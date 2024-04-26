using Application.Responses;
using Application.Modules.Users.Models;
using Application.Abstractions.MediatR;

namespace Application.Modules.Users.Queries;

public record GetAllUserQuery
    : IQuery<DomainResponse<PaginatedResult<List<UserDto>>>>
{
    public int Start { get; set; }
    public int Limit { get; set; }
    public string? OrderBy { get; set; }
    public string? Order { get; set; }

    public bool IsValidCommand { get; private set; }
    private bool CheckValidity() => Start > -1 && Limit > 1;
    public GetAllUserQuery(int start,
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