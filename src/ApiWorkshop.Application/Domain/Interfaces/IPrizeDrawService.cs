using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces;

public interface IPrizeDrawService
{
    Task<BaseResponse<PrizeDraw>> Draw(string name);
    BaseResponse<List<PrizeDrawResponse>> Get(PrizeDrawFilter filter);
    Task Reset();
}
