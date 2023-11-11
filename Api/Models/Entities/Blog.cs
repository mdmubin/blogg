namespace Api.Models.Entities;

public class Blog : ContentBase
{
    public DateTime PostedDateTime { get; set; }

    public bool IsPublic { get; set; }

    public string Heading { get; set; } = null!;

    public string? PageTitle { get; set; }

    public string? CoverImgUrl { get; set; }

    // public string UrlSlug { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string Content { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = null!;
}
