using ApiWorkshop.Application.Domain.Filters.Base;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ApiWorkshop.Application.Domain.Filters;

public class EntrantFilter : FilterBase
{
    [FromQuery(Name = "mall_id")]
    public Guid? MallId { get; set; }
    [FromQuery(Name = "draw_id")]
    public Guid? DrawId { get; set; }
}
