using Dima.Core.Entities;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Core.Handlers.EntityHandlers;

public interface IEntityHandler<T> where T : BaseEntity
{
    Task<PagedApiResponse<List<T>>> GetAll(GetAllRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResponse<T>> GetBySeq(GetBySeqRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<T>> Create(CreateRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResponse<T>> Update(UpdateRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request, CancellationToken cancellationToken = default);
}
