using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrawsController : ControllerBase
{
    private readonly IPrizeDrawService _prizeDrawService;
    public DrawsController(IPrizeDrawService prizeDrawService)
    {
        _prizeDrawService = prizeDrawService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<PrizeDrawResponse?>), StatusCodes.Status200OK)]
    public ActionResult<BaseResponse<PrizeDrawResponse>> Get([FromQuery] PrizeDrawFilter filter)
        => _prizeDrawService.Get(filter);

    [HttpPost("{name}")]
    [ProducesResponseType(typeof(BaseResponse<PrizeDrawDataResponse>), StatusCodes.Status200OK)]
    public async Task<BaseResponse<PrizeDrawDataResponse>> Draw(string name)
        => await _prizeDrawService.Draw(name);

    [HttpDelete("reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Reset()
    {
        await _prizeDrawService.Reset();
        return Ok();
    }
}
