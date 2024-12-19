using Dima.Core.Entities;

namespace Dima.Core.Requests;

public record PostRequest<T>(string UserId, T Entity) : BaseRequest(UserId) where T : BaseEntity;