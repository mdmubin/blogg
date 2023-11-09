using System.Security.Cryptography;
using Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BloggContext : IdentityDbContext<User, UserRole, Guid>
{

    public BloggContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Reply> Replies { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRoleId = Guid.NewGuid();

        builder.Entity<UserRole>().HasData(
            new() { Id = adminRoleId, Name = "admin", NormalizedName = "ADMIN" },
            new() { Id = Guid.NewGuid(), Name = "moderator", NormalizedName = "MODERATOR" },
            new() { Id = Guid.NewGuid(), Name = "user", NormalizedName = "USER" }
        );

        // admin user seed

        var admin = new User
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@blogg.com",
            NormalizedEmail = "ADMIN@BLOGG.COM",
            EmailConfirmed = true,
        };

        var hasher = new PasswordHasher<User>();
        admin.PasswordHash = hasher.HashPassword(admin, "1234");
        admin.SecurityStamp = Convert.ToBase64String(RandomNumberGenerator.GetBytes(24));

        builder.Entity<User>().HasData(admin);

        // add admin role to the user
        builder.Entity<IdentityUserRole<Guid>>()
            .HasData(new IdentityUserRole<Guid>() { UserId = admin.Id, RoleId = adminRoleId });
    }
}