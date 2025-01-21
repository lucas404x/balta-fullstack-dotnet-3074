
using System.Net;
using Dima.Core.Exceptions;

namespace Dima.Api.Domain.Exceptions;

public sealed class InvalidProvidedColumnNameException(
    string tableName, string property, string? message = null, HttpStatusCode code = HttpStatusCode.BadRequest) 
    : DomainException(message ?? $"A propriedade {property} não existe na tabela {tableName}.", code);
