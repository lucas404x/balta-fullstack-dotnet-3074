namespace Dima.Core.Requests;

public record UpdateRequest<T>(T Entity, List<string> changedFields) : BaseRequest;