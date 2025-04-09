using System.Xml.XPath;
using CSharpFunctionalExtensions;
using Shared;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Domain.Students;

public class Parent : Entity<ParentId>, ISoftDeletable
{
    public Parent()
    {
    }

    // private readonly List<Student> _students = [];
    private Parent(
        ParentId id,
        CitizenId citizenId,
        Email email,
        PhoneNumber phoneNumber,
        Address address)
        : base(id)
    {
        CitizenId = citizenId;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public Address Address { get; private set; } = null!;

    public string City => Address.CityName;

    public PhoneNumber PhoneNumber { get; private set; } = null!;

    public CitizenId CitizenId { get; private set; } = null!;

    public Email? Email { get; private set; }

    // public IReadOnlyList<Student> Students => _students;
    public List<Student> Children { get; private set; } = [];

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOn { get; private set; }

    public static Result<Parent, ErrorList> Create(
        ParentId id,
        CitizenId citizenId,
        Email email,
        PhoneNumber phoneNumber,
        Address address)
    {
        return new Parent(id, citizenId, email, phoneNumber, address);
    }

    public void Update(
        CitizenId citizenId,
        Email email,
        PhoneNumber phoneNumber,
        Address address)
    {
        CitizenId = citizenId;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

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