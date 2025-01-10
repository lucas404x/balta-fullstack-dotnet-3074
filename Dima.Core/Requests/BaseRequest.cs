using Dima.Core.Entities;

namespace Dima.Core.Requests;

public abstract record BaseRequest(string UserId);

public abstract record BaseRequestWithEntity<T>(string UserId, T Entity) : BaseRequest(UserId) where T : BaseEntity;