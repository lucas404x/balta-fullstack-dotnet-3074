using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class ApiResponse<T>
{
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    [JsonConstructor]
    public ApiResponse()
    {
        
    }
    
    public ApiResponse(T result)
    {
        Result = result;
    }

    public ApiResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
    
}