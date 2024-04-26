using MediatR;
using System.Data;
using Domain.Entities;
using Application.Responses;
using Microsoft.Data.SqlClient;
using Application.Modules.Users.Commands;
using Application.Abstractions.Services;
using Application.Services.Cryptography;

namespace Application.Modules.Users.Handlers;
public class UserCommandHandlers(
    IPersistanceService<User> persistance) :
    IRequestHandler<CreateCustomerCommand, DomainResponse<Guid>>,
    IRequestHandler<CreateAdminCommand, DomainResponse<Guid>>,
    IRequestHandler<CreateVendorCommand, DomainResponse<Guid>>,
    IRequestHandler<UpdateUserCommand, DomainResponse<Unit>>,
    IRequestHandler<DeleteUserCommand, DomainResponse<Unit>>

{
    [Obsolete]
    public async Task<DomainResponse<Guid>> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = PasswordHasher.HashPassword(request.Password);

            List<SqlParameter> parameters =
            [
                new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@FirstName", request.FirstName),
                new SqlParameter("@LastName", request.LastName),
                new SqlParameter("@DateOfBirth", request.DateOfBirth),
                new SqlParameter("@PhoneNumber", request.PhoneNumber),
                new SqlParameter("@IsActive", false),
                new SqlParameter("@CreatedDate",  DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@LastModifiedDate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@Gender", request.Gender),
                new SqlParameter("@Street", request.Street),
                new SqlParameter("@City", request.City),
                new SqlParameter("@State", request.State),
                new SqlParameter("@Country", request.Country),
                new SqlParameter("@PostalCode", request.PostalCode),
                new SqlParameter("@ShippingAddress", request.ShippingAddress),
                new SqlParameter("@CompanyName", request.CompanyName),
                new SqlParameter("@Description", request.Description),
                new SqlParameter("@Discriminator", request.Discriminator.ToString())
            ];

            Guid createdId = (Guid)await persistance.ExecuteCommandAsync(
                sql: "uspInsertUser",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return new DomainResponse<Guid>
            {
                Success = true,
                Message = "User has been successfully created.",
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
    
    [Obsolete]
    public async Task<DomainResponse<Guid>> Handle(
        CreateVendorCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = PasswordHasher.HashPassword(request.Password);

            List<SqlParameter> parameters =
            [
                new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@FirstName", request.FirstName),
                new SqlParameter("@LastName", request.LastName),
                new SqlParameter("@DateOfBirth", request.DateOfBirth),
                new SqlParameter("@PhoneNumber", request.PhoneNumber),
                new SqlParameter("@IsActive", false),
                new SqlParameter("@CreatedDate",  DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@LastModifiedDate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@Gender", request.Gender),
                new SqlParameter("@Street", request.Street),
                new SqlParameter("@City", request.City),
                new SqlParameter("@State", request.State),
                new SqlParameter("@Country", request.Country),
                new SqlParameter("@PostalCode", request.PostalCode),
                new SqlParameter("@ShippingAddress", request.ShippingAddress),
                new SqlParameter("@CompanyName", request.CompanyName),
                new SqlParameter("@Description", request.Description),
                new SqlParameter("@Discriminator", request.Discriminator.ToString())
            ];

            Guid createdId = (Guid)await persistance.ExecuteCommandAsync(
                sql: "uspInsertUser",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return new DomainResponse<Guid>
            {
                Success = true,
                Message = "User has been successfully created.",
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
    
    [Obsolete]
    public async Task<DomainResponse<Guid>> Handle(
        CreateAdminCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = PasswordHasher.HashPassword(request.Password);
            string roleIds = string.Join(',', request.Roles.Select(x => x.Id));
            List<SqlParameter> parameters =
            [
                new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@FirstName", request.FirstName),
                new SqlParameter("@LastName", request.LastName),
                new SqlParameter("@DateOfBirth", request.DateOfBirth),
                new SqlParameter("@PhoneNumber", request.PhoneNumber),
                new SqlParameter("@IsActive", false),
                new SqlParameter("@CreatedDate",  DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@LastModifiedDate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@Gender", request.Gender),
                new SqlParameter("@Street", request.Street),
                new SqlParameter("@City", request.City),
                new SqlParameter("@State", request.State),
                new SqlParameter("@Country", request.Country),
                new SqlParameter("@PostalCode", request.PostalCode),
                new SqlParameter("@ShippingAddress", request.ShippingAddress),
                new SqlParameter("@CompanyName", request.CompanyName),
                new SqlParameter("@Description", request.Description),
                new SqlParameter("@Discriminator", request.Discriminator.ToString()),
                new SqlParameter("@RoleIds", roleIds),
            ];

            Guid createdId = (Guid)await persistance.ExecuteCommandAsync(
                sql: "uspInsertUser",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return new DomainResponse<Guid>
            {
                Success = true,
                Message = "User has been successfully created.",
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
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parameters =
            [
                new SqlParameter
                {
                    ParameterName = "@IsUpdated",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@FirstName", request.FirstName),
                new SqlParameter("@LastName", request.LastName),
                new SqlParameter("@DateOfBirth", request.DateOfBirth),
                new SqlParameter("@PhoneNumber", request.PhoneNumber),
                new SqlParameter("@IsActive", request.IsActive),
                new SqlParameter("@LastModifiedDate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                new SqlParameter("@Gender", request.Gender),
                new SqlParameter("@Street", request.Street),
                new SqlParameter("@City", request.City),
                new SqlParameter("@State", request.State),
                new SqlParameter("@Country", request.Country),
                new SqlParameter("@PostalCode", request.PostalCode),
                new SqlParameter("@ShippingAddress", request.ShippingAddress),
                new SqlParameter("@CompanyName", request.CompanyName),
                new SqlParameter("@Description", request.Description)
            ];

            dynamic isUpdated = await persistance.ExecuteCommandAsync(
                sql: "uspUpdateUser",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return new DomainResponse<Unit>
            {
                Success = isUpdated,
                Message = isUpdated ?
                  "User has been successfully updated." :
                  "User updation unsuccessful.",
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
        DeleteUserCommand request,
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
                sql: "uspDeleteUser",
             parameters: [.. parameters],
                cancellationToken: cancellationToken);

            return new DomainResponse<Unit>
            {
                Success = isDeleted,
                Message = isDeleted ?
                  "User has been successfully deleted." :
                  "User deletion unsuccessful.",
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
