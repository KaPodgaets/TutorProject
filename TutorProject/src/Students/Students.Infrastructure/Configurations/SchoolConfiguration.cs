using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Students.Domain.Schools;

namespace Students.Infrastructure.Configurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.ToTable("schools");

        // Primary Key
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SchoolId.Create(value))
            .HasColumnName("id")
            .IsRequired();

        // Organization name
        builder.Property(s => s.OrganizationName)
            .HasColumnName("organization_name")
            .HasMaxLength(200)
            .IsRequired();

        // Ministry Organization Id
        builder.Property(s => s.MinistryOrganizationId)
            .HasConversion(
                id => id.Value,
                value => MinistryOrganizationId.Create(value).Value)
            .HasColumnName("ministry_organization_id")
            .IsRequired();

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

        // Soft delete tracking
        builder.Property(s => s.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(s => s.DeletedOn)
            .HasColumnName("deleted_on");
    }
}