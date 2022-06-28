using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces;
public interface IGiftService
{
    Task<Gift> Create(GiftRequest giftRequest);
    Task<Gift> Delete(Guid id, GiftRequest giftRequest);
    Task<DefaultResponse<GiftResponse>> ReadById(Guid id);
    Task<Gift> Update(Guid id, GiftRequest giftRequest);
}