using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureLogging();
        builder.ConfigureSwagger();
        builder.ConfigureDatabase();

        var app = builder.Build();

        // TODO: remove this. recreates db on every run (very bad idea, but need it just for now)
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BloggContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}
