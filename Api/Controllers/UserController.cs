using Api.Data;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AuthService authService;
    private readonly IMapper mapper;
    private readonly RepositoryManager manager;

    public UserController(AuthService authService, IMapper mapper, RepositoryManager manager)
    {
        this.authService = authService;
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser([FromBody] UserRegistrationRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }

        var (status, id, _) = await authService.Register(request);

        if (!status.Succeeded)
        {
            foreach (var err in status.Errors)
            {
                ModelState.AddModelError(err.Code, err.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    [HttpGet("{username}")]
    [Authorize(Roles = "admin, moderator")]
    public async Task<ActionResult> GetUserByUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username is empty");
        }

        var user = await manager.Users
            .FindByCondition(u => u.UserName == username)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        var userResult = mapper.Map<UserResponse>(user);

        return Ok(userResult);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> UserLogin([FromBody] UserAuthRequest request)
    {
        var (validated, user, roles) = await authService.ValidateUser(request);
        if (!validated)
        {
            return Unauthorized("Incorrect email / password");
        }

        var token = authService.GenerateJwtTokenResponse(user!.Id, user.UserName!, roles!);

        return Ok(token);
    }
}
