using FluentValidation;
using Framework.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Shared.Abstractions;
using Students.Presentation;
using Users.Infrastructure.Postgres.Options;
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
        services.AddApplicationLayers(configuration);

        services.AddUsersModule(configuration)
            .AddStudentsModule(configuration);

        return services;
    }

    private static IServiceCollection AddApplicationLayers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assemblies = new[]
        {
            typeof(Users.Application.DependencyInjection).Assembly,
            typeof(Students.Application.DependencyInjection).Assembly
        };

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

    private static IServiceCollection AddAuthorizationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(
                options =>
                {
                    var jwtOptions = configuration
                                         .GetSection(JwtOptions.SECTION_NAME)
                                         .Get<JwtOptions>()
                                     ?? throw new ApplicationException("missing JwtOptions");

                    options.TokenValidationParameters = JwtValidationParametersFactory.CreateWithLifeTime(jwtOptions);
                });

        services.AddAuthorization();

        return services;
    }
}