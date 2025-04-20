using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tutors.Application.Database;
using Tutors.Domain;

namespace Tutors.Infrastructure.DbContext;

public class TutorsDbContext(string connectionString) : Microsoft.EntityFrameworkCore.DbContext, ITutorsReadDbContext
{
    // Write
    public DbSet<Tutor> Tutors => Set<Tutor>();

    // Queries
    IQueryable<Tutor> ITutorsReadDbContext.Tutors => Tutors.AsQueryable().AsNoTracking();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TutorsDbContext).Assembly,
            type => type.FullName?.Contains("Configurations") ?? false);

        modelBuilder.HasDefaultSchema("tutors");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}