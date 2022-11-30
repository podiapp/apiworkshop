using ApiWorkshop.Application.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities;

public class Entrant : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(11)]
    public string? Cpf { get; set; }
    [Required]
    [StringLength(11)]
    public string? Phone { get; set; }

    public virtual Draw? Draw { get; set; }
}
