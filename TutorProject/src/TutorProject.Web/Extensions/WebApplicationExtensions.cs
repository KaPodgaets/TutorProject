using Serilog;
using Shared.Abstractions;

namespace TutorProject.Web.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ConfigureApplication(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();

        // auto migrations and auto seeding
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
        {
            await app.ApplyMigrations();

            await app.Services.RunAutoSeeding();

            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "TutorProject"));
        }

        // app.ConfigureCors();
        // app.UseAuthentication();
        // app.UseScopeDataMiddleware();
        // app.UseAuthorization();
        app.MapControllers();
    }

    private static async Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var migrators = scope.ServiceProvider.GetServices<IMigrator>();
        foreach (var migrator in migrators)
        {
            await migrator.MigrateAsync();
        }
    }

    private static async Task RunAutoSeeding(this IServiceProvider serviceProvider)
    {
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<IAutoSeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}