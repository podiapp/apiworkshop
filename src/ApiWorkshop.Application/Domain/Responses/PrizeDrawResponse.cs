using ApiWorkshop.Application.Domain.Entities;

namespace ApiWorkshop.Application.Domain.Responses;

public class PrizeDrawResponse
{
    public PrizeDrawResponse(Guid id,
                             string name,
                             string gift,
                             DateTime createdAt)
    {
        Id = id;
        Name = name;
        Gift = gift;
        CreatedAt = createdAt;
    }

    public PrizeDrawResponse(PrizeDraw prize)
    {
        Id = prize.Id;
        Name = prize.Name;
        Gift = prize.Gift?.Name ?? "";
        CreatedAt = prize.CreatedAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Gift { get; set; }
    public DateTime CreatedAt { get; set; }

    public static List<PrizeDrawResponse> GetResponseFromList(List<PrizeDraw> prizes)
        => prizes.Select(p => new PrizeDrawResponse(p.Id, p.Name, p.Gift?.Name ?? "", p.CreatedAt)).ToList();
}
