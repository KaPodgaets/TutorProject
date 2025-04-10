using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using Shared.Database;
using Shared.Enums;
using Users.Application.Abstractions;
using Users.Application.Database;
using Users.Infrastructure.Postgres.DbContext;
using Users.Infrastructure.Postgres.JwtProvider;
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
        services.AddOptions(configuration)
            .AddDatabase()
            .AddDbContext(configuration)
            .AddRepositories()
            .AddUsersSeeding()
            .AddIdentityManagers();
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UsersDbContext>(_ => new UsersDbContext(configuration.GetConnectionString("Database")!));
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Users);
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IRefreshSessionsRepository, RefreshSessionRepository>();

        return services;
    }

    private static IServiceCollection AddIdentityManagers(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AdminOptions>(
            options =>
            {
                options.Email = Environment.GetEnvironmentVariable("ADMIN_USER_EMAIL");
                options.Password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
            });

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SECTION_NAME));

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, UsersMigrator>();

        return services;
    }

    private static IServiceCollection AddUsersSeeding(this IServiceCollection services)
    {
        services.AddScoped<IAutoSeeder, UsersSeeder>();
        services.AddScoped<UsersSeedingService>();

        return services;
    }
}