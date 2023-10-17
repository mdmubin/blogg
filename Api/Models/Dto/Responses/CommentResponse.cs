namespace Api.Models.Dto.Responses;

public class CommentResponse
{
    public Guid Id { get; set; }

    public bool Edited { get; set; }

    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }
}
