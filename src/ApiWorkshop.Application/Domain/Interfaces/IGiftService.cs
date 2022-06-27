using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Requests;

namespace ApiWorkshop.Application.Domain.Interfaces;
public interface IGiftService
{
    Task<Gift> Insert(GiftRequest giftRequest);
}