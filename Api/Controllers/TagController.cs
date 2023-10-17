using Api.Data;
using Api.Models.Entities;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/tags")]
public class TagController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;


    public TagController(RepositoryManager repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


    [HttpGet( Name = "Get All Tags")]
    public async Task<ActionResult> GetAllTags()
    {
        var tagList = await repository.Tags.GetAll()
            .ToListAsync();

        var tagResult = mapper.Map<ICollection<TagResponse>>(tagList);

        return Ok(tagResult);
    }

    [HttpGet("{id:guid}", Name = "Get By Id")]
    public async Task<ActionResult> GetTagById(Guid id)
    {
        var tag = await repository.Tags
            .FindByCondition(t => t.Id == id)
            .FirstOrDefaultAsync();

        if (tag == null)
        {
            return NotFound();
        }

        var tagResult = mapper.Map<TagResponse>(tag);

        return Ok(tagResult);
    }

    [HttpPost]
    public async Task<ActionResult> PostTag([FromBody] TagCreateRequest request)
    {
        var newTag = mapper.Map<Tag>(request);

        var existing = await repository.Tags
            .FindByCondition(t => t.NormalizedName == newTag.NormalizedName)
            .FirstOrDefaultAsync();

        if (existing != null)
        {
            return Conflict();
        }

        repository.Tags.Create(newTag);
        await repository.SaveChanges();

        var newTagResponse = mapper.Map<TagResponse>(newTag);

        return CreatedAtAction(nameof(GetTagById), new { id = newTag.Id }, newTagResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateTag(Guid id, [FromBody] TagUpdateRequest request)
    {
        var existing = await repository.Tags
            .FindByCondition(t => t.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            return NotFound();
        }

        var newTag = mapper.Map<Tag>(request);
        newTag.Id = id;

        repository.Tags.Update(newTag);
        await repository.SaveChanges();

        return Accepted();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteTag(Guid id)
    {
        var existing = await repository.Tags
            .FindByCondition(t => t.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            return NotFound();
        }

        repository.Tags.Delete(existing);
        await repository.SaveChanges();

        return Accepted();
    }
}