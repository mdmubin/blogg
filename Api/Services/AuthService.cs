using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services;

public class AuthService
{
    private readonly IConfiguration jwtConfig;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;


    public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration appConfig)
    {
        this.jwtConfig = appConfig.GetSection("JwtConfiguration");
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async Task<(IdentityResult Status, Guid Id, UserResponse? UserResponse)> Register(UserRegistrationRequest request)
    {
        var user = mapper.Map<User>(request);

        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "registered_user");
            var userResult = mapper.Map<UserResponse>(user);
            return (result, user.Id, userResult);
        }

        return (result, Guid.Empty, null);
    }

    public async Task<(bool validated, User? user)> ValidateUser(UserAuthRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        var validUser = user != null && await userManager.CheckPasswordAsync(user, request.Password);

        return (validUser, user);
    }

    public AuthResponse GenerateJwtTokenResponse(Guid userId, string username)
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["JwtSecret"]!));
        var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["ExpiresAfter"]!));

        var userClaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwtConfig["Subject"]!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: userClaims,
            expires: expiresAt,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthResponse { Token = tokenHandler.WriteToken(token), Expires = expiresAt, };
    }
}