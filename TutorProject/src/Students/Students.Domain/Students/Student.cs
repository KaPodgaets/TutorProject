using CSharpFunctionalExtensions;
using Shared;

namespace Students.Domain.Students;

public class Student : Entity<StudentId>, ISoftDeletable
{
    public Student(StudentId id, FullName fullName, string citizenId)
        : base(id)
    {
        FullName = fullName;
        CitizenId = citizenId;
    }

    public FullName FullName { get; }

    public string CitizenId { get; set; }

    public string? PassportNumber { get; set; }

    public Guid? SchoolId { get; set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOn { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOn = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedOn = null;
    }
}

public class CitizenId : ComparableValueObject
{
    public CitizenId(string citizenId)
    {
        Value = citizenId;
    }

    public string Value { get; init; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents() =>
        throw new NotImplementedException();
}