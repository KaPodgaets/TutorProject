using Students.Domain.Students.Ids;

namespace Students.Domain.Students;

public class ParentStudent
{
    public ParentId ParentId { get; set; } = null!;

    public StudentId StudentId { get; set; } = null!;

    public Parent Parent { get; set; } = null!;

    public Student Student { get; set; } = null!;
}