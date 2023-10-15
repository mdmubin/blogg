namespace Api.Models.Dto.Requests;

public class CommentCreateRequest
{
    public string Content { get; set; } = null!;
}

public class CommentUpdateRequest
{
    public string Content { get; set; } = null!;
}