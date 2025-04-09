using CSharpFunctionalExtensions;
using Shared;
using Shared.ResultPattern;
using Shared.ValueObjects;

namespace Students.Domain.Schools;

public class School : Entity<SchoolId>, ISoftDeletable
{
    private School(
        SchoolId id,
        Address address,
        MinistryOrganizationId ministryOrganizationId,
        string organizationName)
        : base(id)
    {
        Address = address;
        MinistryOrganizationId = ministryOrganizationId;
        OrganizationName = organizationName;
    }

    public Address Address { get; private set; }

    public MinistryOrganizationId MinistryOrganizationId { get; private set; }

    public string OrganizationName { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOn { get; private set; }

    public static Result<School, ErrorList> Create(
        SchoolId id,
        Address address,
        MinistryOrganizationId ministryOrganizationId,
        string organizationName)
    {
        if (string.IsNullOrWhiteSpace(organizationName))
            return Errors.General.ValueIsRequired(nameof(organizationName)).ToErrorList();

        return new School(id, address, ministryOrganizationId, organizationName);
    }

    public void Update(Address address, MinistryOrganizationId ministryOrganizationId, string organizationName)
    {
        Address = address;
        MinistryOrganizationId = ministryOrganizationId;
        OrganizationName = organizationName;
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