using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Students.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}