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

        builder.Services.AddScoped<RepositoryManager>();
        builder.Services.AddAutoMapper(typeof(DataMapper));
    }

    public static void ConfigureAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();

        builder.Services.AddIdentity<User, UserRole>(opt =>
        {
            // Password settings
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequiredLength = 6;
            opt.Password.RequiredUniqueChars = 1;

            // Lockout settings
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.AllowedForNewUsers = true;

            // User settings
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            opt.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<BloggContext>();

        builder.Services.AddScoped<JwtService>();
        builder.Services.AddScoped<AuthService>();
    }

    public static void ConfigureCookies(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureApplicationCookie(opt =>
        {
            // TODO: Configure cookies here
        });
    }
}