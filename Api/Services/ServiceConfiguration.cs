using System.Text;
using Api.Data;
using Api.Models.Entities;
using Api.Services.AuthHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description = "Place JWT bearer token here",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            },
                        },
                        Array.Empty<string>()
                    },
                });
            });
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
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var jwtConfiguration = builder.Configuration.GetSection("JwtConfiguration");
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtConfiguration["Audience"]!,
                    ValidIssuer = jwtConfiguration["Issuer"]!,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfiguration["JwtSecret"]!)
                    ),
                };
            });

        builder.Services
            .AddIdentityCore<User>(opt =>
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
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<BloggContext>();

        builder.Services
            .AddAuthorization(opt =>
            {
                opt.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                opt.AddPolicy(
                    "AuthorOnlyPolicy",
                    policy => policy.Requirements.Add(new AuthRequirements.Author())
                );
                opt.AddPolicy(
                    "AuthorAndModeratorPolicy",
                    policy => policy.Requirements.Add(new AuthRequirements.AuthorAndModerator())
                );
            });

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddSingleton<IAuthorizationHandler, ContentDeleteHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, ContentUpdateHandler>();
    }

    public static void ConfigureCookies(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureApplicationCookie(opt =>
        {
            // TODO: Configure cookies here
        });
    }

    public static void ConfigureCorsPolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opt => opt.AddPolicy("DefaultPolicy", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));
    }
}