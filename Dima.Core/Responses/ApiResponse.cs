namespace Dima.Core.Responses;

public class ApiResponse<T>
{
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }
}