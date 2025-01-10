using System.Net;

namespace Dima.Api.Domain.Exceptions;

public class DomainException(
    string? Message, 
    HttpStatusCode Code = HttpStatusCode.BadRequest) : Exception(Message)
{
}
