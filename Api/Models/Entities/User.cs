using Microsoft.AspNetCore.Identity;

namespace Api.Models.Entities;

public class User : IdentityUser<Guid>
{
    public string? ProfilePictureUrl { get; set; }
}
