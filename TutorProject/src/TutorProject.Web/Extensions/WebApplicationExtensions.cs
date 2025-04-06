using Serilog;

namespace TutorProject.Web.Extensions;

public static class WebApplicationExtensions
{
    public static async Task Configure(this WebApplication app)
    {
        await Task.CompletedTask;

        // if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
        // {
        //     await app.Services.RunMigrations();
        //     await app.Services.RunAutoSeeding();
        // }
        app.UseSwaggerUI();

        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();

        // app.ConfigureCors();
        // app.UseAuthentication();
        // app.UseScopeDataMiddleware();
        // app.UseAuthorization();
        app.MapControllers();
    }
}