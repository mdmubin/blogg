using Api.Data;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public static class ServiceConfiguration
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
    }

    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }

    public static void ConfigureDataServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DbConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Database Config Failure. Database Connection string not found");
        }

        builder.Services.AddDbContext<BloggContext>(opt =>
        {
            opt.UseMySQL(connectionString);
        });

        builder.Services.AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<BloggContext>();

        builder.Services.AddScoped<RepositoryManager>();
        builder.Services.AddAutoMapper(typeof(DataMapper));
    }
}