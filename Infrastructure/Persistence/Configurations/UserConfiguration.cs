using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.FirstName).IsRequired();

        builder.Property(u => u.LastName).IsRequired();

        builder.Property(u => u.DateOfBirth).IsRequired();

        builder.Property(u => u.PhoneNumber);

        builder.Property(u => u.IsActive).IsRequired();

        builder.Property(u => u.CreatedDate).IsRequired();

        builder.Property(u => u.LastModifiedDate).IsRequired();

        builder.Property(u => u.Gender)
               .IsRequired()
               .HasColumnType("nvarchar(10)");

        builder.Property(u => u.Street);

        builder.Property(u => u.City);

        builder.Property(u => u.State);

        builder.Property(u => u.Country);

        builder.Property(u => u.PostalCode);
    }
}