using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrawsController : ControllerBase
{
    private readonly IDrawService _repo;

    public DrawsController(IDrawService repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("")]
    public ActionResult<BaseResponse<List<DrawResponse>>> Get([FromQuery] DrawFilter filter)
        => _repo.Read(filter);

    [HttpPost]
    [Route("")]
    public async Task<Draw> Post(DrawRequest request)
        => await _repo.Create(request);

    [HttpPut]
    [Route("{id}")]
    public async Task<Draw> Put(Guid id, DrawRequest request)
        => await _repo.Update(id, request);

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.Delete(id);
        return Ok();
    }
}
