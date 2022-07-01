using ApiWorkshop.Application.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiWorkshop.Application.Domain.Entities;

public class PrizeDraw : BaseEntity
{
    public string Code { get; set; }
    public Guid? GiftId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public virtual Gift? Gift { get; set; }

    public PrizeDraw()
    {
        Code = $"{GetChars()}-{GetChars()}-{GetChars()}";
    }

    private static string GetChars() => Guid.NewGuid().ToString()[..3].ToUpper();
}
