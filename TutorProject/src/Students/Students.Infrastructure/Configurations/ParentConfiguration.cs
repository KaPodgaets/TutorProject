using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects;
using Students.Domain.Students;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Infrastructure.Configurations;

public class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.ToTable("parents");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => ParentId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(p => p.CitizenId)
            .HasConversion(
                c => c.Value,
                v => CitizenId.Create(v).Value)
            .IsRequired()
            .HasColumnName("citizen_id");

        builder.Property(p => p.Email)
            .HasConversion(
                e => e != null ? e.Value : null,
                v => v != null ? Email.Create(v).Value : null)
            .HasColumnName("email");

        builder.Property(p => p.PhoneNumber)
            .HasConversion(
                p => p.Value,
                v => PhoneNumber.Create(v).Value)
            .IsRequired()
            .HasColumnName("phone_number");

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

        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.Property(p => p.DeletedOn)
            .HasColumnName("deleted_on")
            .IsRequired(false);
    }
}