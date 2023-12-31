namespace Api.Models.Dto.Responses;

public class BlogResponse
{
    public Guid Id { get; set; }

    public DateTime PostedDateTime { get; set; }

    public string Heading { get; set; } = null!;

    public string? PageTitle { get; set; }

    public string? CoverImgUrl { get; set; }

    public string Content { get; set; } = null!;

    public ICollection<TagResponse> Tags { get; set; } = null!;
}
