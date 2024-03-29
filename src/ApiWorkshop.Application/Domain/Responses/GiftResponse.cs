﻿using ApiWorkshop.Application.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Responses;

public class GiftResponse
{
    public GiftResponse(Guid id,
                        string name,
                        string photo,
                        string description,
                        int quantity,
                        int availableQuantity,
                        int winnersQuantity,
                        Status status)
    {
        Id = id;
        Name = name;
        Photo = photo;
        Description = description;
        Quantity = quantity;
        AvailableQuantity = availableQuantity;
        WinnersQuantity = winnersQuantity;
        Status = status;
    }
    public GiftResponse(Guid id,
                        string name,
                        string photo,
                        string description,
                        int quantity,
                        Status status)
    {
        Id = id;
        Name = name;
        Photo = photo;
        Description = description;
        Quantity = quantity;
        Status = status;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int AvailableQuantity { get; }
    public int WinnersQuantity { get; }
    public Status Status { get; set; }
}
