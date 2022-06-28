using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces;
public interface IGiftService
{
    Task<Gift> Create(GiftRequest giftRequest);
    Task<Gift> Delete(Guid id, GiftRequest giftRequest);
    BaseResponse<List<GiftResponse>> Read(GiftFilter filter);
    Task<BaseResponse<GiftResponse>> ReadById(Guid id);
    Task<Gift> Update(Guid id, GiftRequest giftRequest);
}