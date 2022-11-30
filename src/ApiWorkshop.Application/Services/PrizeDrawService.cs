using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Extensions;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Services;

public class PrizeDrawService : IPrizeDrawService
{
    private readonly IBaseRepository<PrizeDraw> _prizeDrawRepository;
    private readonly IBaseRepository<Gift> _giftRepository;

    public PrizeDrawService(IBaseRepository<PrizeDraw> prizeDrawRepository, IBaseRepository<Gift> giftRepository)
    {
        _prizeDrawRepository = prizeDrawRepository;
        _giftRepository = giftRepository;
    }

    public async Task<BaseResponse<PrizeDrawsResponse>> Draw(string name)
    {
        var gifts = _giftRepository.Where(g => g.Status == Domain.Enums.Status.ACTIVE && (g.PrizeDraws == null || g.Quantity - g.PrizeDraws.Count(pd => pd.GiftId == g.Id) > 0))
            .ToList();

        Guid giftId = GetPrizeGift(gifts) ??
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "Erro ao gerar prêmio.");

        Gift fakeGift = Gift.GetFakeGift();

        var prize = new PrizeDraw()
        {
            Name = name,
            GiftId = (fakeGift.Id != giftId) ? giftId : null,
            Status = Domain.Enums.Status.ACTIVE,
        };

        _prizeDrawRepository.Insert(prize);
        await _prizeDrawRepository.SaveChangesAsync();
        prize = await _prizeDrawRepository.Where(prize.Id);

        return new(new PrizeDrawsResponse(prize.Id,
                                    prize.Name,
                                    prize.Gift?.Name ?? "",
                                    prize.GiftId,
                                    prize.CreatedAt,
                                    prize.Gift?.Photo,
                                    "Shopping Palladium Curitiba",
                                    prize.CreatedAt.AddDays(7),
                                    prize.Code));
    }

    public BaseResponse<PrizeDrawResponse> Get(PrizeDrawFilter filter)
    {
        var prizes = Filter(_prizeDrawRepository.Where().Include(p => p.Gift), filter, out int count, out int totalCount).ToList();
        return new(new PrizeDrawResponse(prizes), count, totalCount);
    }

    public async Task Reset()
    {
        _prizeDrawRepository.DeleteRange(_prizeDrawRepository.Where().ToList());
        await _prizeDrawRepository.SaveChangesAsync();
    }

    private static Guid? GetPrizeGift(List<Gift> gifts)
    {
        gifts.Add(Gift.GetFakeGift());
        List<Gift>? list = new();
        foreach (Gift gift in gifts)
        {
            int count = gift.Quantity;
            while (count > 0)
            {
                list.Add(gift);
                count--;
            }
        }

        list.Shuffle();

        return list.FirstOrDefault()?.Id;
    }


    private static IQueryable<PrizeDraw> Filter(IQueryable<PrizeDraw> entities, PrizeDrawFilter filter, out int count, out int totalCount)
    {
        if (filter.Search != null)
            entities = entities.Where(p => p.Name != null && p.Name.ToLower().Contains(filter.Search!.ToLower().Trim()) ||
                                           p.Gift != null && p.Gift.Name != null && p.Gift.Name.ToLower().Contains(filter.Search!.ToLower().Trim()));

        totalCount = entities.Count();

        entities = GetOrder(entities, filter.Order, filter.Desc);

        if (filter.Skip != null)
            entities = entities.Skip(filter.Skip.Value);
        if (filter.Take != null)
            entities = entities.Take(filter.Take.Value);

        count = entities.Count();

        return entities;
    }

    private static IQueryable<PrizeDraw> GetOrder(IQueryable<PrizeDraw> entities, string order, bool desc)
    {
        return order switch
        {
            "name" => (!desc ? entities.OrderBy(p => p.Name) : entities.OrderByDescending(p => p.Name)),
            "gift" => (!desc ? entities.OrderBy(p => p.Gift!.Name) : entities.OrderByDescending(p => p.Gift!.Name)),
            "status" => (!desc ? entities.OrderBy(p => p.Status) : entities.OrderByDescending(p => p.Status)),
            "date" => (!desc ? entities.OrderBy(p => p.CreatedAt) : entities.OrderByDescending(p => p.CreatedAt)),
            _ => entities.OrderByDescending(p => p.CreatedAt),
        };
    }
}
