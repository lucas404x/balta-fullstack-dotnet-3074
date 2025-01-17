using Dima.Core.Entities;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests;

public class GetAllRequest<T> : PagedRequest 
    where T : BaseEntity
{
    public List<RequestOrderByProp>? OrderByProperties { get; set; }

    [JsonIgnore]
    public string TableName { get; } = typeof(T).Name;

    public override string? Validate()
    {
        if (OrderByProperties is not null && OrderByProperties.Count > 1)
        {
            var groupedProps = OrderByProperties
                .Select(x => x.Property.ToLower())
                .GroupBy(x => x)
                .ToList();

            if (groupedProps.Count != OrderByProperties.Count)
                return "There are duplicated properties in the OrderByProperties list.";
        }
        return base.Validate();
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