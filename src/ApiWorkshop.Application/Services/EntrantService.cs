using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;

namespace ApiWorkshop.Application.Services;

public class EntrantService : IEntrantService
{
    private readonly IBaseRepository<Entrant> _entrantRepository;

    public EntrantService(IBaseRepository<Entrant> entrantRepository)
    {
        _entrantRepository = entrantRepository;
    }

    public async Task<Entrant> Create(EntrantRequest request)
    {
        Entrant entrant = new()
        {
            Cpf = request.Cpf,
            Name = request.Name,
            Phone = request.Phone,
            DrawId = request.DrawId
            
        };

        _entrantRepository.Insert(entrant);
        await _entrantRepository.SaveChangesAsync();

        return entrant;
    }

    public BaseResponse<List<EntrantResponse>> Read(EntrantFilter filter)
    {
        var entrants = Filter(_entrantRepository.Where(), filter, out int count, out int totalCount).ToList();

        List<EntrantResponse> response = entrants.Select(e =>
        {
            return new EntrantResponse(e.Id, e.Name, e.Cpf, e.Phone, e.DrawId);
        }).ToList();

        return new() { Data = response, Count = count, TotalCount = totalCount };
    }

    public async Task<Entrant> Update(Guid id, EntrantRequest request)
    {
        var entrant = _entrantRepository.Where(p => p.Id == id).FirstOrDefault();
        if (entrant == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Participante não encontrado.");

        entrant.Name = request.Name;
        entrant.Cpf = request.Cpf;
        entrant.Phone = request.Phone;
        entrant.DrawId = request.DrawId;

        _entrantRepository.Update(entrant);
        await _entrantRepository.SaveChangesAsync();

        return entrant;
    }

    public async Task Delete(Guid id)
    {
        var entrant = _entrantRepository.Where(p => p.Id == id).FirstOrDefault();
        if (entrant == null)
            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Participante não encontrado.");

        entrant.UpdatedAt = DateTime.UtcNow;

        await _entrantRepository.Delete(id);
        await _entrantRepository.SaveChangesAsync();
    }

    private static IQueryable<Entrant> Filter(IQueryable<Entrant> entities, EntrantFilter filter, out int count, out int totalCount)
    {
        if (filter.Search != null)
            entities = entities.Where(p => p.Name != null && p.Name.ToLower().Contains(filter.Search!.ToLower().Trim()));

        totalCount = entities.Count();

        entities = GetOrder(entities, filter.Order, filter.Desc);

        if (filter.Skip != null)
            entities = entities.Skip(filter.Skip.Value);
        if (filter.Take != null)
            entities = entities.Take(filter.Take.Value);

        count = entities.Count();

        return entities;
    }

    private static IQueryable<Entrant> GetOrder(IQueryable<Entrant> entities, string order, bool desc)
    {
        return order switch
        {
            "name" => (!desc ? entities.OrderBy(p => p.Name) : entities.OrderByDescending(p => p.Name)),
            "cpf" => (!desc ? entities.OrderBy(p => p.Cpf) : entities.OrderByDescending(p => p.Cpf)),
            _ => entities.OrderByDescending(p => p.CreatedAt),
        };
    }
}
