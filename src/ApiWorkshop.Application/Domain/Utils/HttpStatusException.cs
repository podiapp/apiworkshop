using System.Net;

namespace ApiWorkshop.Application.Domain.Utils;
public class HttpStatusException : Exception
{
    public HttpStatusCode Status { get; private set; }

    public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
    {
        Status = status;
    }
}