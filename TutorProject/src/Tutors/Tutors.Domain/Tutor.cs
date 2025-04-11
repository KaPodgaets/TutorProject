using CSharpFunctionalExtensions;
using Shared;
using Shared.ResultPattern;
using Shared.ValueObjects;

namespace Tutors.Domain;

public class Tutor : Entity<TutorId>, ISoftDeletable
{
    private readonly List<Guid> _studentIds = [];

    public Tutor(TutorId id)
        : base(id)
    {
    }

    public Tutor(
        TutorId id,
        FullName fullName,
        CitizenId citizenId,
        Address address,
        Email email,
        PhoneNumber phoneNumber)
        : base(id)
    {
        CitizenId = citizenId;
        FullName = fullName;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; set; } = null!;

    public CitizenId CitizenId { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public PhoneNumber PhoneNumber { get; set; } = null!;

    public Email Email { get; set; } = null!;

    public IReadOnlyList<Guid> StudentIds => _studentIds.AsReadOnly();

    public int WorkLoadInHours { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOn { get; private set; }

    public static Result<Tutor, ErrorList> Create(
        TutorId id,
        FullName fullName,
        CitizenId citizenId,
        Address address,
        Email email,
        PhoneNumber phoneNumber)
    {
        if (TutorId.None == id)
            return Errors.General.ValueIsRequired(nameof(TutorId)).ToErrorList();

        if (FullName.None == fullName)
            return Errors.General.ValueIsRequired(nameof(FullName)).ToErrorList();

        if (CitizenId.None == citizenId)
            return Errors.General.ValueIsRequired(nameof(CitizenId)).ToErrorList();

        if (Email.None == citizenId)
            return Errors.General.ValueIsRequired(nameof(CitizenId)).ToErrorList();

        if (PhoneNumber.None == phoneNumber)
            return Errors.General.ValueIsRequired(nameof(CitizenId)).ToErrorList();

        return new Tutor(id, fullName, citizenId, address, email, phoneNumber);
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