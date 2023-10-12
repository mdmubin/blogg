using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entities;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; }
}