using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class Tag
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NormalizedName { get; set; } = null!;

    public ICollection<Blog> Posts { get; set; } = null!;
}