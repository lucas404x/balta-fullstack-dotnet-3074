using System.Net;

namespace Dima.Api.Domain.Exceptions;

public class DomainException(
    string? message, 
    HttpStatusCode code = HttpStatusCode.BadRequest) : Exception(message)
{
    public HttpStatusCode Code { get; private set; } = code;
}
