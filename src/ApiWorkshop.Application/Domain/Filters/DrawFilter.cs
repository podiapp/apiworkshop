using ApiWorkshop.Application.Domain.Enums;
using ApiWorkshop.Application.Domain.Filters.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorkshop.Application.Domain.Filters;

public class DrawFilter : FilterBase
{
    [FromQuery(Name = "id")]
    public Guid? Id { get; set; }
    [FromQuery(Name = "mall_id")]
    public Guid? MallId { get; set; }
    [FromQuery(Name = "status")]
    public List<Status>? Status { get; set; }
}
