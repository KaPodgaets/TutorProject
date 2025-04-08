using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;
using Users.Domain.ValueObjects;

namespace Users.Infrastructure.Postgres.Configurations;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable("refresh_sessions");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(u => u.Id);

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,                  // Convert to store in DB (UserId -> Guid)
                value => UserId.Create(value))   // Convert from DB (Guid -> UserId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}