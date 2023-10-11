using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }

    //

    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDateTime { get; set; }

    //

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }

    public ICollection<Reply> Replies { get; set; } = null!;
}