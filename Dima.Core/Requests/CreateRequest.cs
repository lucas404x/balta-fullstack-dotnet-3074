using Dima.Core.Entities;

namespace Dima.Core.Requests;

public record CreateRequest<T>(string UserId, T Entity) : BaseRequest(UserId) where T : BaseEntity;