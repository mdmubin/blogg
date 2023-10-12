namespace Api.Models.Entities;

public class Comment : EntityBase
{
    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }

    //

    public Guid BlogId { get; set; }
    public Blog? Blog { get; set; }

    public ICollection<Reply> Replies { get; set; } = null!;
}