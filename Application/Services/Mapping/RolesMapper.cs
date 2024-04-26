using Application.Modules.Roles.Models;
using Domain.Entities;

namespace Application.Services.Mapping;
public static class RolesMapper
{
    public static RoleDto AsDto(this Role role)
        => new(role.Id, role.Name, role.Description);

    public static Role AsEntity(this RoleDto role)
        => new()
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
}
