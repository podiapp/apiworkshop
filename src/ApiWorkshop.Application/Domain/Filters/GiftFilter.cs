using ApiWorkshop.Application.Domain.Enums;
using ApiWorkshop.Application.Domain.Filters.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Domain.Filters
{
    public class GiftFilter : FilterBase
    {
        [FromQuery(Name = "status")]
        public Status? Status { get; set; }
        [FromQuery(Name = "has_fake_gift")]
        public bool? HasFakeGift { get; set; }
    }
}
