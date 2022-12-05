using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces;

public interface IEntrantService
{
    Task<Entrant> Create(EntrantRequest request);
    Task Delete(Guid id);
    BaseResponse<List<EntrantResponse>> Read(EntrantFilter filter);
    Task<Entrant> Update(Guid id, EntrantRequest request);
}
