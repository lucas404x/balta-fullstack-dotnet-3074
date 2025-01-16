namespace Dima.Core.Requests;

public class GetBySeqRequest : BaseRequest
{
    public required long Seq { get; set; }
}
