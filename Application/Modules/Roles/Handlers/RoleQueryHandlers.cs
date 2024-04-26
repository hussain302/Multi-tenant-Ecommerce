using MediatR;
using Application.Responses;
using Application.Modules.Roles.Models;
using Application.Modules.Roles.Queries;
using Microsoft.Data.SqlClient;
using System.Data;
using Application.Services.Mapping;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Modules.Roles.Handlers;
public class RoleQueryHandlers(
     IPersistanceService<Role> persistance) :
     IRequestHandler<GetRoleQueryById, DomainResponse<RoleDto>>,
     IRequestHandler<GetAllRoleQuery, DomainResponse<PaginatedResult<List<RoleDto>>>>
{
    public async Task<DomainResponse<RoleDto>> Handle(
        GetRoleQueryById request,
        CancellationToken cancellationToken)
    {
        try
        {
            var role = await persistance.ExecuteNonCollectionQueryAsync(
                sql: "uspFetchRoleById",
                parameters: [
                    new ("@Id", request.Id)
                ],
                cancellationToken: cancellationToken);

            if (role is null)
                return new DomainResponse<RoleDto>
                {
                    Success = false,
                    Message = "Roles not found successfully.",
                    Data = new RoleDto(
                        Guid.Empty,
                        string.Empty,
                        string.Empty)
                };

            RoleDto roleDto = new(
                role.Id, 
                role.Name, 
                role.Description);

            return new DomainResponse<RoleDto>
            {
                Success = true,
                Message = "Roles found successfully.",
                Data = roleDto
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<RoleDto>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = new RoleDto(
                    Guid.Empty, 
                    string.Empty, 
                    string.Empty)
            };
        }
    }

    public async Task<DomainResponse<PaginatedResult<List<RoleDto>>>> Handle(
        GetAllRoleQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parameters =
            [
                new SqlParameter("@start", request.Start),
                new SqlParameter("@limit", request.Limit),
                new SqlParameter("@orderBy", request.OrderBy),
                new SqlParameter("@order", request.Order),
                new SqlParameter
                {
                    ParameterName = "@totalCount",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                }
            ];

            var roles = await persistance.ExecuteCollectionQueryAsync(
                sql: "uspFetchAllRoles",
                parameters: [.. parameters],
                cancellationToken: cancellationToken);

            if (roles.Items is null || roles.Items.Count == 0)
                return new DomainResponse<PaginatedResult<List<RoleDto>>>
                {
                    Success = false,
                    Message = "Roles not found successfully.",
                    Data = new PaginatedResult<List<RoleDto>>
                    {
                        Items = [],
                        ItemCount = 0
                    }
                };

            PaginatedResult<List<RoleDto>>? rolesDto = new()
            {
                Items = roles.Items
                .Select(x => x.AsDto())
                .ToList(),
                ItemCount = roles.ItemCount
            };

            return new DomainResponse<PaginatedResult<List<RoleDto>>>
            {
                Success = true,
                Message = "Roles found successfully.",
                Data = rolesDto
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<PaginatedResult<List<RoleDto>>>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = new PaginatedResult<List<RoleDto>>
                {
                    Items = [],
                    ItemCount = 0
                }
            };
        }
    }
}