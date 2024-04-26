using Domain.Entities;

namespace Domain.Aggregates;
public class UserRole
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public Guid? AssignedBy { get; set; }
    public DateTime AssignedAt { get; set; }
}