using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects;
using Tutors.Domain;

namespace Tutors.Infrastructure.Configurations;

public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.ToTable("tutors");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => TutorId.Create(value))
            .IsRequired()
            .HasColumnName("id");

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

        builder.Property(s => s.CitizenId)
            .HasConversion(
                c => c.Value,
                v => CitizenId.Create(v).Value)
            .IsRequired()
            .HasColumnName("citizen_id");

        builder.OwnsOne(
            p => p.Address,
            address =>
            {
                address.Property(a => a.CityName)
                    .HasColumnName("city")
                    .IsRequired();

                address.Property(a => a.StreetName)
                    .HasColumnName("street_name")
                    .IsRequired();

                address.Property(a => a.StreetCode)
                    .HasColumnName("street_code")
                    .IsRequired();

                address.Property(a => a.BuildingNumber)
                    .HasColumnName("building_number")
                    .IsRequired();

                address.Property(a => a.BuildingLetter)
                    .HasColumnName("building_letter")
                    .IsRequired(false);
            });

        builder.Property(p => p.Email)
            .HasConversion(
                e => e.Value,
                v => Email.Create(v).Value)
            .HasColumnName("email");

        builder.Property(p => p.PhoneNumber)
            .HasConversion(
                p => p.Value,
                v => PhoneNumber.Create(v).Value)
            .IsRequired()
            .HasColumnName("phone_number");

        builder.Property(s => s.WorkLoadInHours)
            .HasColumnName("tutor_hours_needed");

        builder.Property(s => s.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.Property(s => s.DeletedOn)
            .HasColumnName("deleted_on");

        builder.Property(typeof(List<Guid>), "_studentIds")
            .HasColumnName("student_ids")
            .HasColumnType("jsonb");
    }
}