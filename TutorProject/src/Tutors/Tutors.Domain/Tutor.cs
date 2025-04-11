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

    public Tutor(TutorId id, FullName fullName, CitizenId citizenId, Address address)
        : base(id)
    {
        CitizenId = citizenId;
        FullName = fullName;
        Address = address;
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

    public static Result<Tutor, ErrorList> Create(TutorId id, FullName fullName, CitizenId citizenId, Address address)
    {
        if (TutorId.None == id)
            return Errors.General.ValueIsRequired(nameof(TutorId)).ToErrorList();

        if (FullName.None == fullName)
            return Errors.General.ValueIsRequired(nameof(FullName)).ToErrorList();

        if (CitizenId.None == citizenId)
            return Errors.General.ValueIsRequired(nameof(CitizenId)).ToErrorList();

        return new Tutor(id, fullName, citizenId, address);
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