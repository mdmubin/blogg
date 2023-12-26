namespace Api.Models.Dto.Responses;

public class AuthResponse
{
    public string Token { get; set; } = null!;

    public string Username { get; set; } = null!;
}