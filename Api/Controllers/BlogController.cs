using Api.Data;
using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly RepositoryManager repository;

    public BlogController(RepositoryManager repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBlog([FromBody] BlogCreateRequest request)
    {
        var newBlog = mapper.Map<Blog>(request);

        repository.Blogs.Create(newBlog);
        await repository.SaveChanges();

        var newBlogResult = mapper.Map<BlogResponse>(newBlog);

        return CreatedAtAction(nameof(GetBlog), new { id = newBlog.Id }, newBlogResult);
    }


    [HttpGet("{id:guid}")]
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

        var orphanedComments = await repository.Comments
            .FindByCondition(comment => comment.BlogId == id)
            .ToListAsync();

        repository.Blogs.Delete(blog);
        repository.Comments.DeleteAll(orphanedComments);

        await repository.SaveChanges();
        return Accepted();
    }
}