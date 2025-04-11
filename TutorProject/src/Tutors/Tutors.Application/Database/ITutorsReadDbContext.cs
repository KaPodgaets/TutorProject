using Tutors.Domain;

namespace Tutors.Application.Database;

public interface ITutorsReadDbContext
{
    IQueryable<Tutor> Tutors { get; }
}