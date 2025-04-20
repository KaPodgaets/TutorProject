using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutors.Infrastructure;

namespace Tutors.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddTutorsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureLayer(configuration);

        return services;
    }
}