using ApiWorkshop.Application.Domain.Entities;
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

        [HttpGet("{id}")]
        public async Task<BaseResponse<GiftResponse>> Get(Guid id) => await _repo.ReadById(id);

        [HttpPost("")]
        public async Task<Gift> Post(GiftRequest giftRequest) => await _repo.Create(giftRequest);

        [HttpPut("{id}")]
        public async Task<Gift> Put(Guid id, GiftRequest giftRequest) => await _repo.Update(id, giftRequest);

        [HttpDelete("{id}")]
        public async Task<Gift> Delete(Guid id, GiftRequest giftRequest) => await _repo.Delete(id, giftRequest);
    }


}
