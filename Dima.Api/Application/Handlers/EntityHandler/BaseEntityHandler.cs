using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Application.Handlers.EntityHandler;

abstract public class BaseEntityHandler<TEntity>(IEntityRepository<TEntity> repository) : IEntityHandler<TEntity>
    where TEntity : BaseEntity
{
    public async Task<PagedApiResponse<List<TEntity>>> GetAll(GetAllRequest<TEntity> request)
    {
        var response = new PagedApiResponse<List<TEntity>>();
        var (values, totalRecords) = await repository.GetAll(request);
        response.Result = values;
        response.TotalRecords = totalRecords;
        response.CurrentPage = request.PageNumber;
        return response;
    }

    public async Task<ApiResponse<TEntity>> GetBySeq(GetBySeqRequest request)
    {
        var response = new ApiResponse<TEntity>
        {
            Result = await repository.GetById(request)
        };
        return response;
    }

    public async Task<ApiResponse<TEntity>> Create(CreateRequest<TEntity> request)
    {
        var response = new ApiResponse<TEntity>
        {
            Result = await repository.Create(request)
        };
        return response;
    }

    public async Task<ApiResponse<TEntity>> Update(UpdateRequest<TEntity> request)
    {
        var response = new ApiResponse<TEntity>
        {
            Result = await repository.Update(request)
        };
        return response;
    }

    public async Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request)
    {
        var response = new ApiResponse<bool>
        {
            Result = await repository.Delete(request)
        };
        return response;
    }
}
