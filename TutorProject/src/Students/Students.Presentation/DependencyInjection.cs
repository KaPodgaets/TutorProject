using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Students.Infrastructure;

namespace Students.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureLayer(configuration);

        return services;
    }
}