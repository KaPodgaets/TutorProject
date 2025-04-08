using CSharpFunctionalExtensions;
using Shared;
using Shared.ValueObjects;

namespace Students.Domain.Students;

public class Parent : Entity<ParentId>, ISoftDeletable
{
    public Parent(List<Student> students, Address address):base(id)
    {
        Students = students;
        Address = address;
    }

    public List<Student> Students { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }
    public void Delete() => throw new NotImplementedException();

    public void Restore() => throw new NotImplementedException();
}