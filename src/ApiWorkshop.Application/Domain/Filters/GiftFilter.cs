using ApiWorkshop.Application.Domain.Enums;
using ApiWorkshop.Application.Domain.Filters.Base;

namespace ApiWorkshop.Application.Domain.Filters
{
    public class GiftFilter : FilterBase
    {
        public Status Status { get; set; }
    }
}
