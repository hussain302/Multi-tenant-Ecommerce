using Domain.Aggregates;

namespace Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<UserRole>? UserRoles { get; set; }
}