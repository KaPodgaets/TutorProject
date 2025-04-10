using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using Shared.Database;
using Shared.Enums;
using Students.Application.Database;
using Students.Infrastructure.DbContext;
using Students.Infrastructure.Migrator;

namespace Students.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDatabase()
            .AddDbContext(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentsRepository, StudentsRepository>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<StudentsDbContext>(
            _ => new StudentsDbContext(configuration.GetConnectionString("Database")!));

        services.AddScoped<IStudentsDbContext, StudentsDbContext>(
            _ => new StudentsDbContext(configuration.GetConnectionString("Database")!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Students);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, StudentsMigrator>();

        return services;
    }
}