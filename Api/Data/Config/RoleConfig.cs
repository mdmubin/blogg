using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Config;

public class RoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(new[]
        {
            new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "admin",
            },
            new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "Registered User",
                NormalizedName = "registered_user",
            },
            new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "Visitor",
                NormalizedName = "visitor",
            },
        }); 
    }
}