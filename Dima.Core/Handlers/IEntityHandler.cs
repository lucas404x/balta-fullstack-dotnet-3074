using Dima.Core.Entities;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface IEntityHandler<T> where T : BaseEntity
{
    Task<ApiResponse<List<T>>> GetAll(GetAllRequest<T> request);
    Task<ApiResponse<T>> GetById(GetBySeqRequest request);
    Task<ApiResponse<T>> Create(CreateRequest<T> request);
    Task<ApiResponse<T>> Update(UpdateRequest<T> request);
    Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request);
}
