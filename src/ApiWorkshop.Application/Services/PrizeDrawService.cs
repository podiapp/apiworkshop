using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Extensions;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Utils;

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

    public async Task<PrizeDraw> Draw(string name)
    {
        var gifts = _giftRepository.Where(g => g.Status == Domain.Enums.Status.ACTIVE && (g.PrizeDraws == null || g.Quantity - g.PrizeDraws.Count(pd => pd.GiftId == g.Id) > 0))
            .ToList();

        Guid giftId = GetPrizeGift(gifts) ??
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "Erro ao gerar prêmio.");

        var prize = new PrizeDraw()
        {
            Name = name,
            GiftId = giftId,
            Status = Domain.Enums.Status.ACTIVE,
        };

        _prizeDrawRepository.Insert(prize);
        await _prizeDrawRepository.SaveChangesAsync();

        return prize;
    }

    private static Guid? GetPrizeGift(List<Gift> gifts)
    {
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
}
