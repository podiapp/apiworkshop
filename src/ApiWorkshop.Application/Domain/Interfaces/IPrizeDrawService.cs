using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces;

public interface IPrizeDrawService
{
    Task<BaseResponse<DrawResponse>> Draw(string name);
    BaseResponse<PrizeDrawResponse> Get(PrizeDrawFilter filter);
    Task Reset();
}
