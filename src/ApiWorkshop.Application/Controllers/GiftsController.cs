using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Domain.Responses;
using ApiWorkshop.Application.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _repo;

        public GiftsController(IGiftService repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public ActionResult<BaseResponse<List<GiftResponse>>> Get([FromQuery] GiftFilter filter) => _repo.Read(filter);

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<GiftResponse>>> GetById(Guid id) => await _repo.ReadById(id);

        [HttpPost("")]
        public async Task<ActionResult<BaseResponse<Gift>>> Post(GiftRequest giftRequest) => await _repo.Create(giftRequest);

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Gift>>> Put(Guid id, GiftRequest giftRequest) => await _repo.Update(id, giftRequest);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, GiftRequest giftRequest)
        {
            await _repo.Delete(id, giftRequest);
            return Ok();
        }
    }


}
