using ApiWorkshop.Application.Domain.Filters.Base;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ApiWorkshop.Application.Domain.Filters;

public class EntrantFilter : FilterBase
{
    [FromQuery(Name = "id")]
    public Guid? Id { get; set; }
    [FromQuery(Name = "draw_id")]
    public Guid? DrawId { get; set; }
}
