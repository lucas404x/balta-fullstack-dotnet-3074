using Dima.Core.Entities;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests;

public abstract class BaseRequest
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
}

public abstract class BaseRequestWithEntity<T> : BaseRequest where T : BaseEntity
{
    public required T Entity { get; set; }
}