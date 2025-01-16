namespace Dima.Core.Requests;

public class DeleteBySeqRequest : BaseRequest
{
    public required long Seq { get; set; }
}
