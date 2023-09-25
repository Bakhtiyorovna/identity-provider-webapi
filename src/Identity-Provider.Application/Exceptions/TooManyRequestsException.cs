
using System.Net;

namespace Identity_Provider.Application.Exceptions;

public class TooManyRequestsException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;
    public string TitleMessage { get; protected set; } = String.Empty;
}
