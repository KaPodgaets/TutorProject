using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Students.Domain.Students;
using Students.Domain.Students.Ids;

namespace Students.Infrastructure.Configurations;

public class ParentStudentConfiguration : IEntityTypeConfiguration<ParentStudent>
{
    public void Configure(EntityTypeBuilder<ParentStudent> builder)
    {
        builder.ToTable("parent_students");

        // Composite primary key using raw Guid values
        builder.HasKey(e => new { e.ParentId, e.StudentId });

        // Convert ValueObjects to Guid
        builder.Property(e => e.ParentId)
            .HasConversion(
                id => id.Value,
                value => ParentId.Create(value))
            .HasColumnName("parent_id");

        builder.Property(e => e.StudentId)
            .HasConversion(
                id => id.Value,
                value => StudentId.Create(value))
            .HasColumnName("student_id");

        // builder.HasOne(e => e.Parent)
        //     .WithMany(p => p.Students)
        //     .HasForeignKey(e => e.ParentId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder.HasOne(e => e.Student)
        //     .WithMany(s => s.Parents)
        //     .HasForeignKey(e => e.StudentId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}