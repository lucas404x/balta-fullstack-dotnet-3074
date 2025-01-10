
using System.Net;

namespace Dima.Api.Domain.Exceptions.EntityExceptions;

public sealed class InvalidProvidedColumnNameException(
    string TableName, string Property, string? Message = null, HttpStatusCode Code = HttpStatusCode.BadRequest) 
    : DomainException(Message ?? $"A propriedade {Property} não existe na tabela {TableName}.", Code)
{
}
