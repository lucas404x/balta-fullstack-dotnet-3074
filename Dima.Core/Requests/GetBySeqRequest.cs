namespace Dima.Core.Requests;

public record GetBySeqRequest(string UserId, long Seq) : BaseRequest(UserId);
