using Dima.Core.Entities;

namespace Dima.Core.Requests;

public record UpdateRequest<T>(
    string UserId, T Entity, List<string> ChangedFields) : BaseRequest(UserId) where T : BaseEntity;