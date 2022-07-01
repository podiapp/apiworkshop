using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiWorkshop.Application.Services;

public class GiftService : IGiftService
{
    private readonly IBaseRepository<Gift> _giftBaseRepository;

    public GiftService(IBaseRepository<Gift> giftBaseRepository)
    {
        _giftBaseRepository = giftBaseRepository;
    }

    public async Task<BaseResponse<Gift>> Create(GiftRequest giftRequest)
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

        return new(gift);
    }

    public async Task<BaseResponse<Gift>> Update(Guid id, GiftRequest giftRequest)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        gift.Name = giftRequest.Name;
        gift.Description = giftRequest.Description;
        gift.Photo = giftRequest.Photo;
        gift.Quantity = giftRequest.Quantity;
        gift.Status = giftRequest.Status;

        _giftBaseRepository.Update(gift);
        await _giftBaseRepository.SaveChangesAsync();

        return new(gift);
    }

    public async Task Delete(Guid id, GiftRequest giftRequest)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        gift.UpdatedAt = DateTime.Now;
        await _giftBaseRepository.Delete(id);
        await _giftBaseRepository.SaveChangesAsync();
    }

    public async Task<BaseResponse<GiftResponse>> ReadById(Guid id)
    {
        var gift = await _giftBaseRepository.Where(id);
        if (gift == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Brinde não encontrado.");

        GiftResponse response = new(gift.Id,
            gift.Name,
            gift.Photo,
            gift.Description,
            gift.Quantity,
            gift.Status);

        return new()
        {
            Data = response,
        };
    }
    public BaseResponse<List<GiftResponse>> Read(GiftFilter filter)
    {
        var gifts = Filter(_giftBaseRepository.Where().Include(g => g.PrizeDraws), filter, out int count, out int totalCount).ToList();

        if (filter.HasFakeGift.HasValue && filter.HasFakeGift.Value)
            gifts.Add(Gift.GetFakeGift());

        List<GiftResponse> giftResponse = gifts.Select(g =>
        {
            return new GiftResponse(g.Id,
                                    g.Name,
                                    g.Photo,
                                    g.Description,
                                    g.Quantity,
                                    g.Quantity - (g.PrizeDraws?.Count ?? 0),
                                    g.PrizeDraws?.Count ?? 0,
                                    g.Status);

        }).ToList();

        return new() { Data = giftResponse, Count = count, TotalCount = totalCount };
    }
    private static IQueryable<Gift> Filter(IQueryable<Gift> entities, GiftFilter filter, out int count, out int totalCount)
    {
        if (filter.Search != null)
            entities = entities.Where(p => p.Name != null && p.Name.ToLower().Contains(filter.Search!.ToLower().Trim()));

        if (filter.Status != null)
            entities = entities.Where(p => p.Status == filter.Status);

        totalCount = entities.Count();

        entities = GetOrder(entities, filter.Order, filter.Desc);

        if (filter.Skip != null)
            entities = entities.Skip(filter.Skip.Value);
        if (filter.Take != null)
            entities = entities.Take(filter.Take.Value);

        count = entities.Count();

        return entities;
    }

    private static IQueryable<Gift> GetOrder(IQueryable<Gift> entities, string order, bool desc)
    {
        return order switch
        {
            "name" => (!desc ? entities.OrderBy(p => p.Name) : entities.OrderByDescending(p => p.Name)),
            "status" => (!desc ? entities.OrderBy(p => p.Status) : entities.OrderByDescending(p => p.Status)),
            "date" => (!desc ? entities.OrderBy(p => p.CreatedAt) : entities.OrderByDescending(p => p.CreatedAt)),
            _ => entities.OrderByDescending(p => p.CreatedAt),
        };
    }
}
