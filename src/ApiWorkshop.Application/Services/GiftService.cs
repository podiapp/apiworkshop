using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Services;

public class GiftService : IGiftService
{
    private readonly IBaseRepository<Gift> _giftBaseRepository;

    public GiftService(IBaseRepository<Gift> giftBaseRepository)
    {
        _giftBaseRepository = giftBaseRepository;
    }

    public async Task<Gift> Create(GiftRequest giftRequest)
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

    public async Task<Gift> Update(Guid id, GiftRequest giftRequest)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        gift.Description = giftRequest.Description;
        gift.Photo = giftRequest.Photo;
        gift.Quantity = giftRequest.Quantity;
        gift.Status = giftRequest.Status;

        _giftBaseRepository.Update(gift);
        await _giftBaseRepository.SaveChangesAsync();

        return gift;
    }

    public async Task<Gift> Delete(Guid id, GiftRequest giftRequest)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        gift.UpdatedAt = DateTime.Now;
        await _giftBaseRepository.Delete(id);
        await _giftBaseRepository.SaveChangesAsync();

        return gift;
    }

    public async Task<DefaultResponse<GiftResponse>> ReadById(Guid id)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        GiftResponse response = new(gift.Id,
            gift.Name,
            gift.Description,
            gift.Photo,
            gift.Quantity,
            gift.Status);

        return new()
        {
            Data = response,
        };
    }
}
