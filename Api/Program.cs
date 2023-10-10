using Api.Services;

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
        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}
