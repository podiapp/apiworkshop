using ApiWorkshop.Application.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Responses;

public class GiftResponse
{
    public GiftResponse(Guid id, string name, string photo, string description, int quantity, Status status)
    {
        Id = id;
        Name = name;
        Photo = photo;
        Description = description;
        Quantity = quantity;
        Status = status;
    }

    public Guid Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(300)]
    public string Photo { get; set; } = string.Empty;
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public Status Status { get; set; }
}
