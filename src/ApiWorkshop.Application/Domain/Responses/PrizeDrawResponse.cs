using ApiWorkshop.Application.Domain.Entities;

namespace ApiWorkshop.Application.Domain.Responses;

public class PrizeDrawResponse
{
    public PrizeDrawResponse(List<PrizeDraw> prizes)
    {
        Winners = prizes.Count(p => p.GiftId.HasValue);
        Total = prizes.Count;
        Data = prizes.Select(p => new PrizeDrawDataResponse(p)).ToList();
    }

    public int Winners { get; }
    public int Total { get; }
    public List<PrizeDrawDataResponse> Data { get; }
}

public class PrizeDrawDataResponse
{
    public PrizeDrawDataResponse(Guid id,
                             string name,
                             string? gift,
                             Guid? giftId,
                             DateTime createdAt)
    {
        Id = id;
        Name = name;
        Gift = gift;
        GiftId = giftId;
        CreatedAt = createdAt;
    }

    public PrizeDrawDataResponse(PrizeDraw prize)
    {
        Id = prize.Id;
        Name = prize.Name;
        Gift = prize.Gift?.Name ?? "Não foi desta vez!";
        GiftId = prize.GiftId;
        CreatedAt = prize.CreatedAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Gift { get; set; }
    public Guid? GiftId { get; }
    public DateTime CreatedAt { get; set; }
}