using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.Requests;

public class UserRegistrationRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = null!;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; } = null!;
}


public class UserUpdateRequest
{
    public string? UserName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string? ConfirmPassword { get; set; } = string.Empty;
}

public class UserAuthRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Username cannot be empty")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; } = null!;

    // public bool RememberUser { get; set; } = true;
}