using System.Net;
using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class ApiResponse<T>
{
    private readonly HttpStatusCode _code;

    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    public bool IsSuccess => (int)_code < 400;

    [JsonConstructor]
    public ApiResponse()
    {
        _code = HttpStatusCode.OK;
    }
    
    public ApiResponse(T result, HttpStatusCode code = HttpStatusCode.OK)
    {
        Result = result;
        _code = code;
    }

    public ApiResponse(string errorMessage, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        ErrorMessage = errorMessage;
        _code = code;
    }
    
}