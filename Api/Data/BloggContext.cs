using Api.Data.Config;
using Api.Models.Entities;
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

        builder.ApplyConfiguration(new RoleConfig());
    }
}