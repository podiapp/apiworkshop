using ApiWorkshop.Application.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities.Base;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Status Status { get; set; } = Status.ACTIVE;
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}