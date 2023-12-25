namespace Api.Models.Dto.Responses;

public class UserResponse
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ProfilePictureUrl { get; set; }
}