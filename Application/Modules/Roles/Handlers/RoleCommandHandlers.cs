using MediatR;
using System.Data;
using Application.Responses;
using Microsoft.Data.SqlClient;
using Application.Modules.Roles.Commands;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Modules.Roles.Handlers;

public class RoleCommandHandlers(
        IPersistanceService<Role> persistance) :
        IRequestHandler<CreateRoleCommand, DomainResponse<Guid>>,
        IRequestHandler<UpdateRoleCommand, DomainResponse<Unit>>,
        IRequestHandler<DeleteRoleCommand, DomainResponse<Unit>>
{
    public async Task<DomainResponse<Guid>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parameters =
            [
                new ()
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Output
                },
                new ("@Name", request.Name),
                new ("@Description", request.Description),
            ];

            Guid createdId = (Guid) await persistance.ExecuteCommandAsync(
                sql: "uspInsertRole",
             parameters: [.. parameters],
                cancellationToken: cancellationToken);

            return new DomainResponse<Guid>
            {
                Success = true,
                Message = "Roles has been successfully created.",
                Data = createdId
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<Guid>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = Guid.Empty
            };
        }
    }

    public async Task<DomainResponse<Unit>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parameters =
            [
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@Description", request.Description),
                new SqlParameter
                {
                    ParameterName = "@IsUpdated",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                }
            ];

            dynamic isUpdated = await persistance.ExecuteCommandAsync(
                sql: "uspUpdateRole",
             parameters: [.. parameters],
                cancellationToken: cancellationToken);

            return new DomainResponse<Unit>
            {
                Success = isUpdated,
                Message = isUpdated ? 
                  "Roles has been successfully updated." :
                  "Roles updation unsuccessful.",
                Data = Unit.Value
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<Unit>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = Unit.Value
            };
        }
    }

    public async Task<DomainResponse<Unit>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parameters =
            [
                new SqlParameter("@Id", request.Id),
                new SqlParameter
                {
                    ParameterName = "@IsDeleted",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                }
            ];

            dynamic isDeleted = await persistance.ExecuteCommandAsync(
                sql: "uspDeleteRole",
             parameters: [.. parameters],
                cancellationToken: cancellationToken);

            return new DomainResponse<Unit>
            {
                Success = isDeleted,
                Message = isDeleted ?
                  "Roles has been successfully deleted." :
                  "Roles deletion unsuccessful.",
                Data = Unit.Value
            };
        }
        catch (Exception ex)
        {
            return new DomainResponse<Unit>
            {
                Success = false,
                Message = $"{ex.Message}",
                Data = Unit.Value
            };
        }
    }
}