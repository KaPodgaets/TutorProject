using Students.Domain.Students;

namespace Students.Application.Database;

public interface IStudentsDbContext
{
    IQueryable<Student> Students { get; }

    IQueryable<Parent> Parents { get; }
}