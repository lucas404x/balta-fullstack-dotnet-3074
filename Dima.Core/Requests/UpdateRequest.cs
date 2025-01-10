using Dima.Core.Entities;

namespace Dima.Core.Requests;

public record UpdateRequest<T>(
    string UserId, T Entity, List<string> ChangedFields) : BaseRequestWithEntity<T>(UserId, Entity) where T : BaseEntity;