
using System.Net;

namespace Identity_Provider.Application.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
    public string TitleMessage { get; protected set; } = String.Empty;
}
