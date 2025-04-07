using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects;
using Users.Domain;
using Users.Domain.ValueObjects;

namespace Users.Infrastructure.Postgres.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .IsRequired()
            .HasColumnType("id");

        builder.Property(x => x.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value)
            .IsRequired()
            .HasColumnName("email");

        builder.Property(x => x.PasswordHash)
            .HasColumnName("password");
    }
}