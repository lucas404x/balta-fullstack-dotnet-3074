
using System.Net;

namespace Dima.Api.Domain.Exceptions.EntityExceptions;

public sealed class EntityNotFoundException(
    long Seq, string? Message = null, HttpStatusCode Code = HttpStatusCode.BadRequest) 
    : DomainException(Message ?? $"O registro {Seq} não foi encontrado na base de dados.", Code)
{
}
