using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Database;
using Shared.Enums;
using Students.Infrastructure.DbContext;

namespace Students.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);
        return services;
    }
}