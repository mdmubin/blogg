namespace Api.Models.Entities;

public class Reply : ContentBase
{
    public bool Edited { get; set; }

    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }

    //

    public Guid ParentId { get; set; }
    public Comment Parent { get; set; } = null!;
}