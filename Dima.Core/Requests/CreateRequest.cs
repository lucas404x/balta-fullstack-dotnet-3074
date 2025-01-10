using Dima.Core.Entities;

namespace Dima.Core.Requests;

public record CreateRequest<T>(
    string UserId, T Entity) : BaseRequestWithEntity<T>(UserId, Entity) where T : BaseEntity;