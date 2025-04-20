using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using Shared.Database;
using Shared.Enums;
using Tutors.Application.Database;
using Tutors.Infrastructure.DbContext;
using Tutors.Infrastructure.Migrator;

namespace Tutors.Infrastructure;

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
        services.AddScoped<ITutorsRepository, TutorsRepository>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TutorsDbContext>(
            _ => new TutorsDbContext(configuration.GetConnectionString("Database")!));

        services.AddScoped<ITutorsReadDbContext, TutorsDbContext>(
            _ => new TutorsDbContext(configuration.GetConnectionString("Database")!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Students);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, TutorsMigrator>();

        return services;
    }
}