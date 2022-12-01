using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Requests;

public class EntrantRequest
{
    public EntrantRequest(Guid drawId, string name, string? cpf, string? phone)
    {
        DrawId = drawId;
        Name = name;
        Cpf = cpf;
        Phone = phone;
    }

    [Required]
    public Guid DrawId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(11)]
    public string? Cpf { get; set; }
    [Required]
    [StringLength(11)]
    public string? Phone { get; set; }
}
