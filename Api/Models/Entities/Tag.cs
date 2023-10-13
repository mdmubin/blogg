namespace Api.Models.Entities;

public class Tag : EntityBase
{
    public string TagName { get; set; } = null!;

    public string NormalizedName { get; set; } = null!;

    public ICollection<Blog> Posts { get; set; } = null!;
}