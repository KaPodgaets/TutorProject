using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using TutorProject.Application;
using TutorProject.Application.Database;
using Users.Infrastructure.Postgres.DbContext;
using Users.Infrastructure.Postgres.Migrator;
using Users.Infrastructure.Postgres.Repositories;

namespace Users.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<UsersDbContext>(_ => new UsersDbContext(configuration.GetConnectionString("Database")!));
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, UsersMigrator>();

        // services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);
        return services;
    }
}