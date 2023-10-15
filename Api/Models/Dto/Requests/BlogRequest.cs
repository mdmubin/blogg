using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.Requests;

public class BlogCreateRequest
{
    public bool IsPublic { get; set; } = true;

    [Required(ErrorMessage = "Heading cannot be empty")]
    public string Heading { get; set; } = null!;

    public string? PageTitle { get; set; }

    public string? CoverImgUrl { get; set; }

    public string? ShortDescription { get; set; }

    [Required(ErrorMessage = "Blog content cannot be empty")]
    public string Content { get; set; } = null!;
}

public class BlogUpdateRequest
{
    public bool? IsPublic { get; set; }

    public string? Heading { get; set; }

    public string? PageTitle { get; set; }

    public string? CoverImgUrl { get; set; }

    public string? ShortDescription { get; set; }

    public string? Content { get; set; }
}