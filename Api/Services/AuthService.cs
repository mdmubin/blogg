using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IAuthorizationService authorizationService;


    public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration appConfig, IAuthorizationService authorizationService)
    {
        this.jwtConfig = appConfig.GetSection("JwtConfiguration");
        this.mapper = mapper;
        this.userManager = userManager;
        this.authorizationService = authorizationService;
    }

    public async Task<(IdentityResult Status, Guid Id, UserResponse? UserResponse)> Register(UserRegistrationRequest request)
    {
        var user = mapper.Map<User>(request);

        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "user");
            var userResult = mapper.Map<UserResponse>(user);
            return (result, user.Id, userResult);
        }

        return (result, Guid.Empty, null);
    }

    public async Task<(bool validated, User? user, IList<string>? roles)> ValidateUser(UserAuthRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user != null)
        {
            var validUser = await userManager.CheckPasswordAsync(user, request.Password);
            var userRoles = await userManager.GetRolesAsync(user);

            return (validUser, user, userRoles);
        }

        return (false, null, null);
    }

    public AuthResponse GenerateJwtTokenResponse(Guid userId, string username, IList<string> roles)
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["JwtSecret"]!));
        var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        // var expiresAt = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["ExpiresAfter"]!));

        var userClaims = new List<Claim>
        {
            // new(JwtRegisteredClaimNames.Exp, expiresAt.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Name, username),
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
        };

        foreach (var role in roles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: userClaims,
            // expires: expiresAt,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthResponse { Token = tokenHandler.WriteToken(token), Username = username };
    }

    public bool UserHasPermissions(ClaimsPrincipal userClaim, ContentBase content, string policy)
    {
        var permissionResult = authorizationService.AuthorizeAsync(userClaim, content, policy);
        return permissionResult.Result.Succeeded;
    }
}