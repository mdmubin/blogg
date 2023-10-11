using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BloggContext : DbContext
{
    public BloggContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Reply> Replies { get; set; }
}