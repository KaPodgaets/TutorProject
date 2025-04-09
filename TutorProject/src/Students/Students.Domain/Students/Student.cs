using CSharpFunctionalExtensions;
using Shared;
using Shared.ResultPattern;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Domain.Students;

public class Student : Entity<StudentId>, ISoftDeletable
{
    public Student()
    {
    }

    private readonly List<Parent> _parents = [];

    public Student(
        StudentId id,
        FullName fullName,
        CitizenId citizenId,
        Passport? passport,
        Guid? schoolId)
        : base(id)
    {
        FullName = fullName;
        CitizenId = citizenId;
        Passport = passport;
        SchoolId = schoolId;
    }

    public FullName FullName { get; private set; } = null!;

    public CitizenId CitizenId { get; private set; } = null!;

    public Passport? Passport { get; private set; }

    public Guid? SchoolId { get; private set; }

    public IReadOnlyList<Parent> Parents => _parents;

    public bool IsNeedTutor => TutorHoursNeeded > 0;

    public int TutorHoursNeeded { get; private set; }

    public Guid? TutorId { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOn { get; private set; }

    public static Result<Student, ErrorList> Create(
        StudentId id,
        FullName fullName,
        CitizenId citizenId,
        Passport? passport,
        Guid? schoolId)
    {
        return new Student(id, fullName, citizenId, passport, schoolId);
    }

    public UnitResult<ErrorList> AddParent(Parent parent)
    {
        if (_parents.Any(p => p.Id == parent.Id))
            return UnitResult.Failure(Errors.General.AlreadyExist().ToErrorList());
        if (_parents.Count > 2)
            return UnitResult.Failure(Errors.Students.TooManyParents().ToErrorList());

        _parents.Add(parent);
        return UnitResult.Success<ErrorList>();
    }

    public UnitResult<ErrorList> RemoveParent(Parent parent)
    {
        if (_parents.Any(p => p.Id == parent.Id))
            return UnitResult.Failure(Errors.General.NotFound(parent.Id).ToErrorList());

        _parents.Remove(parent);
        return UnitResult.Success<ErrorList>();
    }

    public UnitResult<ErrorList> AddTutorHours(int hours)
    {
        if (hours > 55)
            return Errors.Students.TutorHoursToBig().ToErrorList();
        TutorHoursNeeded = hours;

        return UnitResult.Success<ErrorList>();
    }

    public void RemoveTutorHours(int hours)
    {
        TutorHoursNeeded = 0;
    }

    public UnitResult<ErrorList> AddTutorId(Guid tutorId)
    {
        if (TutorId.HasValue)
            return UnitResult.Failure(Errors.General.AlreadyExist().ToErrorList());
        TutorId = tutorId;
        return UnitResult.Success<ErrorList>();
    }

    public void RemoveTutor()
    {
        TutorId = null;
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