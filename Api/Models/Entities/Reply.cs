namespace Api.Models.Entities;

public class Reply : EntityBase
{
    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }

    //

    public int ParentId { get; set; }
    public Comment Parent { get; set; } = null!;
}