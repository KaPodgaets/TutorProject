using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TutorProject.Application.Users;
using TutorProject.Application.Users.Commands;

namespace TutorProject.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<CreateUserHandler>();
        return services;
    }
}