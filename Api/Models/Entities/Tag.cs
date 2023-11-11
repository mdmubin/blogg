using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class Tag
{
    [Key]
    public Guid Id { get; set; }

    public string TagName { get; set; } = null!;

    public string NormalizedName { get; set; } = null!;

    public ICollection<Blog> Posts { get; set; } = null!;
}