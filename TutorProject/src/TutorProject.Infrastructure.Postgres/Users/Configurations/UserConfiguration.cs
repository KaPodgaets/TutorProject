using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TutorProject.Domain.Users;

namespace TutorProject.Infrastructure.Postgres.Users.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(p => p.Id);

        builder.Property(x => x.Email)
            .HasColumnName("email");

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Password)
            .HasColumnName("email");
    }
}