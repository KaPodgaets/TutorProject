namespace Students.Domain.Schools;

public class School
{
    public School(
        string ministryOrganizationId,
        string organizationName)
    {
        MinistryOrganizationId = ministryOrganizationId;
        OrganizationName = organizationName;
    }

    public Guid Id { get; set; }

    public string MinistryOrganizationId { get; set; }

    public string OrganizationName { get; set; }
}