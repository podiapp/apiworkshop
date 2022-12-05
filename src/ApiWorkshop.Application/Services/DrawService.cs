using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Services;

public class DrawService : IDrawService
{
    private readonly IBaseRepository<Draw> _drawRepository;

    public DrawService(IBaseRepository<Draw> drawRepository)
    {
        _drawRepository = drawRepository;
    }

    public async Task<Draw> Create(DrawRequest request)
    {
        Draw draw = new()
        {
            PrizeName = request.PrizeName,
            PrizeDescription = request.PrizeDescription,
            PrizePhoto = request.PrizePhoto,
            DrawDate = request.DrawDate,
            MallId = request.MallId
        };

        _drawRepository.Insert(draw);
        await _drawRepository.SaveChangesAsync();

        return draw;
    }

    public BaseResponse<List<DrawResponse>> Read(DrawFilter filter)
    {
        var draws = Filter(_drawRepository.Where(), filter, out int count, out int totalCount).ToList();

        List<DrawResponse> response = draws.Select(d =>
        {
            return new DrawResponse(d.Id,
                                    d.PrizeName,
                                    d.PrizePhoto,
                                    d.PrizeDescription,
                                    d.DrawDate,
                                    d.MallId);
        }).ToList();

        return new() { Data = response, Count = count, TotalCount = totalCount };
    }

    public async Task<Draw> Update(Guid id, DrawRequest request)
    {
        var draw = _drawRepository.Where(p => p.Id == id).FirstOrDefault();
        if (draw == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Sorteio não encontrado.");

        draw.PrizeName = request.PrizeName;
        draw.PrizeDescription = request.PrizeDescription;
        draw.PrizePhoto = request.PrizePhoto;
        draw.DrawDate = request.DrawDate;
        draw.MallId = request.MallId;

        _drawRepository.Update(draw);
        await _drawRepository.SaveChangesAsync();

        return draw;
    }

    public async Task Delete(Guid id)
    {
        var draw = _drawRepository.Where(p => p.Id == id).FirstOrDefault();
        if (draw == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Sorteio não encontrado.");

        draw.UpdatedAt = DateTime.UtcNow;
        await _drawRepository.Delete(id);
        await _drawRepository.SaveChangesAsync();
    }

    private static IQueryable<Draw> Filter(IQueryable<Draw> entities, DrawFilter filter, out int count, out int totalCount)
    {
        if (filter.Search != null)
            entities = entities.Where(p => p.PrizeName != null && p.PrizeName.ToLower().Contains(filter.Search!.ToLower().Trim()));

        if (filter.Status != null)
            entities = entities.Where(p => filter.Status.Contains(p.Status));

        totalCount = entities.Count();

        entities = GetOrder(entities, filter.Order, filter.Desc);

        if (filter.Skip != null)
            entities = entities.Skip(filter.Skip.Value);
        if (filter.Take != null)
            entities = entities.Take(filter.Take.Value);

        count = entities.Count();

        return entities;
    }

    private static IQueryable<Draw> GetOrder(IQueryable<Draw> entities, string order, bool desc)
    {
        return order switch
        {
            "name" => (!desc ? entities.OrderBy(p => p.PrizeName) : entities.OrderByDescending(p => p.PrizeName)),
            "status" => (!desc ? entities.OrderBy(p => p.Status) : entities.OrderByDescending(p => p.Status)),
            "date" => (!desc ? entities.OrderBy(p => p.CreatedAt) : entities.OrderByDescending(p => p.CreatedAt)),
            _ => entities.OrderByDescending(p => p.CreatedAt),
        };
    }
}
