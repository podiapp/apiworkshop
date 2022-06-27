﻿using ApiWorkshop.Application.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities;
public class Gift : BaseEntity
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

    public virtual ICollection<PrizeDraw>? PrizeDraws { get; set; }
}