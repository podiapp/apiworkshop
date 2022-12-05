using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntrantsController : Controller
{
    private readonly IEntrantService _repo;

    public EntrantsController(IEntrantService repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("")]
    public ActionResult<BaseResponse<List<EntrantResponse>>> Get([FromQuery] EntrantFilter filter)
        => _repo.Read(filter);

    [HttpPost]
    [Route("")]
    public async Task<Entrant> Post(EntrantRequest request)
        => await _repo.Create(request);

    [HttpPut]
    [Route("{id}")]
    public async Task<Entrant> Put(Guid id, EntrantRequest request)
        => await _repo.Update(id, request);

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.Delete(id);
        return Ok();
    }
}
