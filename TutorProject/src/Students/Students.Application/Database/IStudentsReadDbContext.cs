using Students.Domain.Students;

namespace Students.Application.Database;

public interface IStudentsReadDbContext
{
    IQueryable<Student> Students { get; }

    IQueryable<Parent> Parents { get; }
}