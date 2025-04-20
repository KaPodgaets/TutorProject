using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tutors.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}