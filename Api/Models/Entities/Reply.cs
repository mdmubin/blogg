using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class Reply
{
    [Key]
    public int Id { get; set; }

    //

    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }

    //

    public int ParentId { get; set; }
    public Comment Parent { get; set; } = null!;
}