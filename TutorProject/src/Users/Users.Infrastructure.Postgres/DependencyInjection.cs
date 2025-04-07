using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using TutorProject.Application;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using Users.Infrastructure.Postgres.DbContext;
using Users.Infrastructure.Postgres.Managers;
using Users.Infrastructure.Postgres.Migrator;
using Users.Infrastructure.Postgres.Options;
using Users.Infrastructure.Postgres.Repositories;
using Users.Infrastructure.Postgres.Seeders;

namespace Users.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions()
            .AddDatabase()
            .AddUsersSeeding()
            .AddIdentityManagers();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<UsersDbContext>(_ => new UsersDbContext(configuration.GetConnectionString("Database")!));

        return services;
    }

    private static IServiceCollection AddIdentityManagers(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.Configure<AdminOptions>(
            options =>
            {
                options.Email = Environment.GetEnvironmentVariable("ADMIN_USER_EMAIL");
                options.Password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
            });

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, UsersMigrator>();

        // services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);
        return services;
    }

    private static IServiceCollection AddUsersSeeding(this IServiceCollection services)
    {
        services.AddScoped<IAutoSeeder, UsersSeeder>();
        services.AddScoped<UsersSeedingService>();

        return services;
    }
}