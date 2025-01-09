namespace Dima.Core.Requests;

public record DeleteBySeqRequest(string UserId, long Seq) : BaseRequest(UserId);
