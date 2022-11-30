using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Domain.Interfaces
{
    public interface IDrawService
    {
        Task<Draw> Create(DrawRequest request);
        Task Delete(Guid id);
        BaseResponse<List<DrawResponse>> Read(DrawFilter filter);
        Task<Draw> Update(Guid id, DrawRequest request);
    }
}
