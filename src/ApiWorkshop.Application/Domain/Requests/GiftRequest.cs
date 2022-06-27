using ApiWorkshop.Application.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Requests;

public class GiftRequest
{
    [Required]
    [StringLength(300)]
    public string Photo { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public Status Status { get; set; }
}
