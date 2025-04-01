namespace TutorProject.Domain;

public class Student
{
    public Student(string firstName, string lastName, string citizenId)
    {
        FirstName = firstName;
        LastName = lastName;
        CitizenId = citizenId;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CitizenId { get; set; }
    public string? PassportNumber { get; set; }
    public Guid? SchoolId { get; set; }
}