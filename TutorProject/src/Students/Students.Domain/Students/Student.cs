using CSharpFunctionalExtensions;
using Shared;

namespace Students.Domain.Students;

public class Student : Entity<StudentId>, ISoftDeletable
{
    public Student(StudentId id, string firstName, string lastName, string citizenId)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        CitizenId = citizenId;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

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