using Dima.Api.Data.Repositories;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Handlers;

public class EntityHandler<T>(IEntityRepository<T> repository) : IEntityHandler<T> where T : BaseEntity
{
    public Task<ApiResponse<T>> Create(CreateRequest<T> request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<T>>> GetAll(GetAllRequest<T> request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<T>> GetById(GetBySeqRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<T>> Update(UpdateRequest<T> request)
    {
        throw new NotImplementedException();
    }
}
