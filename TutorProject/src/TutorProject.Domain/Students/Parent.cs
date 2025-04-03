using TutorProject.Domain.Shared;

namespace TutorProject.Domain.Students;

public class Parent
{
    public Parent(List<Student> students, Address address)
    {
        Students = students;
        Address = address;
    }

    public List<Student> Students { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}