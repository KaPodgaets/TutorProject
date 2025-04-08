using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Infrastructure.Postgres.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(100);
    }
}