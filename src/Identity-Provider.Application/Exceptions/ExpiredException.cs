using System.Net;

namespace Identity_Provider.Application.Exceptions;

public class ExpiredException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.Gone;

    public string TitleMessage { get; protected set; } = String.Empty;
}
