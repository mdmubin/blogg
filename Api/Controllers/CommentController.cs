using Api.Data;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;

    public CommentController(RepositoryManager repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    [HttpGet("{blogId:guid}")]
    public async Task<ActionResult> GetComments(Guid blogId)
    {
        var comments = await repository.Comments
            .FindByCondition(c => c.BlogId == blogId)
            .ToListAsync();

        return Ok(comments);
    }

    [HttpGet("{id:guid}")]
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

    [HttpPost]
    public async Task<ActionResult> CreateComment([FromBody] CommentCreateRequest request)
    {
        var comment = mapper.Map<Comment>(request);

        repository.Comments.Create(comment);
        await repository.SaveChanges();

        var commentResult = mapper.Map<CommentResponse>(comment);

        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentResult);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        var existing = repository.Comments
            .FindByCondition(c => c.Id == id)
            .FirstOrDefault();

        if (existing == null)
        {
            return NotFound();
        }

        repository.Comments.Delete(existing);
        await repository.SaveChanges();

        return Accepted();
    }
}