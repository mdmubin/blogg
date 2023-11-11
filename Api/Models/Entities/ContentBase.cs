using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class ContentBase
{
    [Key]
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public User Author { get; set; } = null!;
}