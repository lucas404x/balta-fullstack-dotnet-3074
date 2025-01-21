using System.Net;
using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class ApiResponse<T>
{
    private readonly HttpStatusCode _code;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Result { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; } = string.Empty;

    public bool IsSuccess => (int)_code < 400;

    [JsonConstructor]
    public ApiResponse()
    {
        _code = HttpStatusCode.OK;
    }
    
    public ApiResponse(T result, string message, HttpStatusCode code = HttpStatusCode.OK)
    {
        Result = result;
        Message = message;
        _code = code;
    }

    public ApiResponse(string errorMessage, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        Message = errorMessage;
        _code = code;
    }
    
}