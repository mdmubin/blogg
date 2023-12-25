namespace Api.Models.Dto.Responses;

public class AuthResponse
{
    public string Token { get; set; } = null!;

    // public DateTime Expires { get; set; }

    public string Username { get; set; } = null!;
}