using Dima.Core.Abstractions;
using Dima.Core.Entities;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests;

public record GetAllRequest<T>(
    string UserId, 
    int PageNumber = 1,
    int PageSize = Configuration.DefaultPageSize, 
    List<RequestOrderByProp>? OrderByProperties = null) : PagedRequest(UserId, PageNumber, PageSize), IRequestValidate 
    where T : BaseEntity
{
    [JsonIgnore]
    public string TableName { get; } = typeof(T).Name;

    public string? Validate()
    {
        if (PageNumber < 1) return "Page number must be greater than 0.";
        if (PageSize < 1) return "Page size must be greater than 0.";
        if (OrderByProperties is not null && OrderByProperties.Count > 1)
        {
            var groupedProps = OrderByProperties
                .Select(x => x.Property.ToLower())
                .GroupBy(x => x)
                .ToList();

            if (groupedProps.Count != OrderByProperties.Count)
                return "There are duplicated properties in the OrderByProperties list.";
        }
        return null;
    }
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