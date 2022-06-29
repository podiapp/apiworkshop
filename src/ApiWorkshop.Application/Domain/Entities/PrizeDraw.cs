using ApiWorkshop.Application.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities;

public class PrizeDraw : BaseEntity
{
    public Guid? GiftId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public virtual Gift? Gift { get; set; }
}
