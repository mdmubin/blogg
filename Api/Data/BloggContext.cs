using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BloggContext : DbContext
{
    public BloggContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}