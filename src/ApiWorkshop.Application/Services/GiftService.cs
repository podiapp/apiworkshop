using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;

namespace ApiWorkshop.Application.Services;

public class GiftService : IGiftService
{
    private readonly IBaseRepository<Gift> _giftBaseRepository;

    public GiftService(IBaseRepository<Gift> giftBaseRepository)
    {
        _giftBaseRepository = giftBaseRepository;
    }

    public async Task<Gift> Insert(GiftRequest giftRequest)
    {
        Gift gift = new()
        {
            Name = giftRequest.Name,
            Description = giftRequest.Description,
            Photo = giftRequest.Photo,
            Quantity = giftRequest.Quantity,
            Status = giftRequest.Status,
        };

        _giftBaseRepository.Insert(gift);
        await _giftBaseRepository.SaveChangesAsync();
        
        return gift;
    }
}
