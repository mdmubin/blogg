using Api.Data;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/replies")]
public class ReplyController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;


    public ReplyController(RepositoryManager repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }


    [HttpGet("{commentId:guid}")]
    public async Task<ActionResult> GetReplies(Guid commentId)
    {
        var comment = await repository.Comments
            .FindByCondition(c => c.Id == commentId)
            .FirstOrDefaultAsync();

        if (comment == null)
        {
            return NotFound();
        }

        var replies = await repository.Replies
            .FindByCondition(r => r.ParentId == commentId)
            .ToListAsync();

        var repliesResult = mapper.Map<ICollection<ReplyResponse>>(replies);

        return Ok(replies);
    }


    [HttpPost("{commentId:guid}")]
    public async Task<ActionResult> CreateReply(Guid commentId, [FromBody] ReplyCreateRequest request)
    {
        var comment = await repository.Comments
            .FindByCondition(c => c.Id == commentId)
            .FirstOrDefaultAsync();

        if (comment == null)
        {
            return NotFound();
        }

        var newReply = mapper.Map<Reply>(
            request,
            opt => opt.AfterMap((src, dest) => dest.ParentId = commentId)
        );

        var replyResult = mapper.Map<ReplyResponse>(newReply);

        return CreatedAtAction(nameof(GetReplies), new { commentId }, replyResult);
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateReply(Guid id, [FromBody] ReplyUpdateRequest request)
    {
        var existing = await repository.Replies
            .FindByCondition(r => r.Id == id, tracking: true)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            return NotFound();
        }

        mapper.Map(request, existing);

        repository.Replies.Update(existing);
        await repository.SaveChanges();

        return Accepted();
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteReply(Guid id)
    {
        var existing = await repository.Replies
            .FindByCondition(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            return NotFound();
        }

        repository.Replies.Delete(existing);
        await repository.SaveChanges();

        return Accepted();
    }
}