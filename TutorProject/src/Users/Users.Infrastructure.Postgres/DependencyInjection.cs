using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TutorProject.Application;
using TutorProject.Application.Users;

namespace Users.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<UsersDbContext>(_ => new UsersDbContext(configuration.GetConnectionString("Database")!));
        return services;
    }
}