namespace TutorProject.Domain.Users;

public class Role
{
    public Role(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}