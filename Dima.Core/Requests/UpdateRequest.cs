using Dima.Core.Entities;

namespace Dima.Core.Requests;

public class UpdateRequest<T> : BaseRequestWithEntity<T> 
    where T : BaseEntity
{
    public List<string> ChangedFields { get; set; } = [];
}