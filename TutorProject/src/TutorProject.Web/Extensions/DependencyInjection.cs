using FluentValidation;
using Shared.Abstractions;
using Users.Presentation;

namespace TutorProject.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();

        services.AddLogging();

        services.AddModules(configuration);

        return services;
    }

    private static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationLayer(configuration);
        services.AddUsersModule(configuration);
        return services;
    }

    private static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assemblies = new[] { typeof(TutorProject.Application.DependencyInjection).Assembly, };

        services.Scan(
            scan => scan.FromAssemblies(assemblies)
                .AddClasses(
                    classes => classes
                        .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        services.Scan(
            scan => scan.FromAssemblies(assemblies)
                .AddClasses(
                    classes => classes
                        .AssignableToAny(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }

    private static IServiceCollection AddLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(o => { o.CombineLogs = true; });
        return services;
    }
}