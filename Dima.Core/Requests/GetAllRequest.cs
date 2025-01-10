using Dima.Core.Entities;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests;

public record GetAllRequest<T>(
    string UserId, 
    int PageNumber = 1,
    int PageSize = Configuration.DefaultPageSize,
    List<RequestOrderByProp>? OrderByProperties = null) : BaseRequest(UserId) where T : BaseEntity
{
    [JsonIgnore]
    public string TableName { get; } = typeof(T).Name;
}

public record RequestOrderByProp
{
    public string Property { get; private set; }
    public bool Ascending { get; private set; }

    public RequestOrderByProp(string property, bool ascending)
    {
        if (string.IsNullOrWhiteSpace(property)) 
            throw new ArgumentNullException(nameof(property));
        
        Property = property;
        Ascending = ascending;
    }


    public static RequestOrderByProp Asc(string property) => new(property, true);
    public static RequestOrderByProp Desc(string property) => new(property, false);
}