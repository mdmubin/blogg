using System.Security.Claims;
using Api.Data;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;
    private readonly AuthService authService;

    public BlogController(RepositoryManager repository, IMapper mapper, AuthService authService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.authService = authService;
    }

    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> CreateBlog([FromBody] BlogCreateRequest request)
    {
        var currentUser = User.FindFirst(ClaimTypes.NameIdentifier);

        var newBlog = mapper.Map<Blog>(request);
        newBlog.AuthorId = Guid.Parse(currentUser!.Value);

        repository.Blogs.Create(newBlog);
        await repository.SaveChanges();

        var newBlogResult = mapper.Map<BlogResponse>(newBlog);

        return CreatedAtAction(nameof(GetBlog), new { id = newBlog.Id }, newBlogResult);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetBlog(Guid id)
    {
        var blog = await repository.Blogs
            .FindByCondition(blog => blog.Id == id)
            .FirstOrDefaultAsync();

        if (blog == null)
        {
            return NotFound();
        }

        return Ok(blog);
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateBlog(Guid id, [FromBody] BlogUpdateRequest request)
    {
        var original = await repository.Blogs
            .FindByCondition(blog => blog.Id == id)
            .FirstOrDefaultAsync();

        if (original == null)
        {
            return NotFound();
        }

        if (!authService.UserHasPermissions(User, original, "AuthorOnlyPolicy"))
        {
            return Unauthorized();
        }

        mapper.Map(request, original);

        repository.Blogs.Update(original);
        await repository.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteBlog(Guid id)
    {
        var blog = await repository.Blogs
            .FindByCondition(blog => blog.Id == id)
            .FirstOrDefaultAsync();

        if (blog == null)
        {
            return NotFound();
        }

        if (!authService.UserHasPermissions(User, blog, "AuthorAndModeratorPolicy"))
        {
            return Unauthorized();
        }

        var orphanedComments = await repository.Comments
            .FindByCondition(comment => comment.BlogId == id)
            .ToListAsync();

        repository.Blogs.Delete(blog);
        repository.Comments.DeleteAll(orphanedComments);

        await repository.SaveChanges();
        return Accepted();
    }
}