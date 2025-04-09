using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Students.Domain.Students;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => StudentId.Create(value))
            .IsRequired()
            .HasColumnName("id");

        builder.Property(s => s.CitizenId)
            .HasConversion(
                c => c.Value,
                v => CitizenId.Create(v).Value)
            .IsRequired()
            .HasColumnName("citizen_id");

        builder.OwnsOne(
            s => s.Passport,
            passport =>
            {
                passport.Property(p => p.Number)
                    .HasColumnName("passport_number")
                    .IsRequired();

                passport.Property(p => p.Country)
                    .HasColumnName("passport_country")
                    .IsRequired();
            });

        builder.Property(s => s.SchoolId)
            .HasColumnName("school_id");

        builder.Property(s => s.TutorId)
            .HasColumnName("tutor_id");

        builder.Property(s => s.TutorHoursNeeded)
            .HasColumnName("tutor_hours_needed");

        builder.Property(s => s.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.Property(s => s.DeletedOn)
            .HasColumnName("deleted_on");

        builder
            .OwnsOne(
                s => s.FullName,
                fullName =>
                {
                    fullName.Property(fn => fn.FirstName)
                        .HasColumnName("first_name")
                        .IsRequired();
                    fullName.Property(fn => fn.LastName)
                        .HasColumnName("last_name")
                        .IsRequired();
                });

        builder.HasMany(r => r.Parents)
            .WithMany(p => p.Children)
            .UsingEntity(j => j.ToTable("StudentParents"));
    }
}