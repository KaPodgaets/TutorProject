using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TutorProject.Application;

namespace TutorProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}