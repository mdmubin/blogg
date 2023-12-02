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
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;
    private readonly AuthService authService;

    public CommentController(RepositoryManager repository, IMapper mapper, AuthService authService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.authService = authService;
    }

    [HttpGet("blog/{blogId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetComments(Guid blogId)
    {
        var comments = await repository.Comments
            .FindByCondition(c => c.BlogId == blogId)
            .ToListAsync();

        return Ok(comments);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetCommentById(Guid id)
    {
        var existing = await repository.Comments
            .FindByCondition(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            return NotFound();
        }

        var commentResult = mapper.Map<CommentResponse>(existing);

        return Ok(commentResult);
    }

    [HttpPost("blog/{blogId:guid}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> CreateComment(Guid blogId, [FromBody] CommentCreateRequest request)
    {
        var blog = await repository.Blogs
            .FindByCondition(b => b.Id == blogId)
            .FirstOrDefaultAsync();

        if (blog == null)
        {
            return NotFound();
        }

        var comment = mapper.Map<Comment>(
            request,
            opt => opt.AfterMap((src, dest) => dest.BlogId = blogId)
        );

        repository.Comments.Create(comment);
        await repository.SaveChanges();

        var commentResult = mapper.Map<CommentResponse>(comment);

        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentResult);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateComment(Guid id, [FromBody] CommentUpdateRequest request)
    {
        var original = await repository.Comments
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

        repository.Comments.Update(original);
        await repository.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        var comment = repository.Comments
            .FindByCondition(c => c.Id == id)
            .FirstOrDefault();

        if (comment == null)
        {
            return NotFound();
        }

        if (!authService.UserHasPermissions(User, comment, "AuthorAndModeratorPolicy"))
        {
            return Unauthorized();
        }

        var orphanedReplies = await repository.Replies
            .FindByCondition(reply => reply.ParentId == id)
            .ToListAsync();

        repository.Comments.Delete(comment);
        repository.Replies.DeleteAll(orphanedReplies);

        await repository.SaveChanges();
        return Accepted();
    }
}