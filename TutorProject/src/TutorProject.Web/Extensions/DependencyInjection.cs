using FluentValidation;
using Shared.Abstractions;
using Users.Presentation;

namespace TutorProject.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(
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
}