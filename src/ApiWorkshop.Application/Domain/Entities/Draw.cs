using ApiWorkshop.Application.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities;

public class Draw : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string? PrizeName { get; set; } = string.Empty;
    [Required]
    [StringLength(300)]
    public string? PrizePhoto { get; set; } = string.Empty;
    [Required]
    [StringLength(500)]
    public string? PrizeDescription { get; set; } = string.Empty;
    [Required]
    public DateTime? DrawDate { get; set; }
    [Required]
    public Guid? MallId { get; set; }

    public virtual List<Entrant>? Entrants { get; set; }
}

