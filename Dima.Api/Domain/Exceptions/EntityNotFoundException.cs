
using System.Net;
using Dima.Core.Exceptions;

namespace Dima.Api.Domain.Exceptions;

public sealed class EntityNotFoundException(
    long seq, string? message = null, HttpStatusCode code = HttpStatusCode.BadRequest) 
    : DomainException(message ?? $"O registro {seq} não foi encontrado na base de dados.", code)
{
}
