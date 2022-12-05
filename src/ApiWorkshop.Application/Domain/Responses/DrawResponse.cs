namespace ApiWorkshop.Application.Domain.Responses;

public class DrawResponse
{
    public DrawResponse(Guid? id, string? prizeName, string? prizePhoto, string? prizeDescription, DateTime? drawDate, Guid? mallId)
    {
        Id = id;
        PrizeName = prizeName;
        PrizePhoto = prizePhoto;
        PrizeDescription = prizeDescription;
        DrawDate = drawDate;
        MallId = mallId;
    }

    public Guid? Id { get; set; }
    public string? PrizeName { get; set; } = string.Empty;
    public string? PrizePhoto { get; set; } = string.Empty;
    public string? PrizeDescription { get; set; } = string.Empty;
    public DateTime? DrawDate { get; set; }
    public Guid? MallId { get; set; }
}
