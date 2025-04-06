using Microsoft.AspNetCore.Mvc.Core;
using Serilog;
using TutorProject.Web.Extensions;

namespace TutorProject.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{builder.Environment.EnvironmentName}.json",
                optional: true,
                reloadOnChange: true);

        builder.Host.UseSerilog(
            (context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddProgramDependencies(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "TutorProject"));
        }

        app.MapControllers();

        await app.Configure();

        app.Run();
    }
}