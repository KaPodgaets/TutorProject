using TutorProject.Domain.Shared;

namespace TutorProject.Domain.Tutors;

public class Tutor
{
    public Tutor(string firstName, string lastName, string citizenId)
    {
        FirstName = firstName;
        LastName = lastName;
        CitizenId = citizenId;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CitizenId { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Guid> StudentIds { get; set; } = [];
}