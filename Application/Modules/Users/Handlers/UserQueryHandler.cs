using Application.Abstractions.Services;
using Application.Modules.Users.Models;
using Application.Modules.Users.Queries;
using Application.Responses;
using Application.Services.Mapping;
using Domain.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Application.Modules.Users.Handlers;
public class UserQueryHandlers(
     IPersistanceService<User> persistance) :
     IRequestHandler<GetUserQueryById, DomainResponse<UserDto>>,
     IRequestHandler<GetAllUserQuery, DomainResponse<PaginatedResult<List<UserDto>>>>
{
    public async Task<DomainResponse<UserDto>> Handle(
        GetUserQueryById request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await persistance.ExecuteNonCollectionQueryAsync(
                sql: "uspFetchUserById",
                parameters: [
                    new ("@Id", request.Id)
                ],
                cancellationToken: cancellationToken);

            if (user is null)
                return new DomainResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found successfully.",
                    Data = default
                };

            UserDto userDto = user.AsDto();

            return new DomainResponse<UserDto>
            {
                Success = true,
                Message = "User found successfully.",
                Data = userDto
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<UserDto>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = default
            };
        }
    }
    public async Task<DomainResponse<PaginatedResult<List<UserDto>>>> Handle(
        GetAllUserQuery request,
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

            var Users = await persistance.ExecuteCollectionQueryAsync(
                sql: "uspFetchAllUsers",
                parameters: [.. parameters],
                cancellationToken: cancellationToken);

            if (Users.Items is null || Users.Items.Count == 0)
                return new DomainResponse<PaginatedResult<List<UserDto>>>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = new PaginatedResult<List<UserDto>>
                    {
                        Items = [],
                        ItemCount = 0
                    }
                };

            var UsersDto = new PaginatedResult<List<UserDto>>
            {
                Items = Users.Items.Select(x => x.AsDto()).ToList(),
                ItemCount = Users.ItemCount
            };

            return new DomainResponse<PaginatedResult<List<UserDto>>>
            {
                Success = true,
                Message = "User found successfully.",
                Data = UsersDto
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<PaginatedResult<List<UserDto>>>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = new PaginatedResult<List<UserDto>>
                {
                    Items = [],
                    ItemCount = 0
                }
            };
        }
    }

}