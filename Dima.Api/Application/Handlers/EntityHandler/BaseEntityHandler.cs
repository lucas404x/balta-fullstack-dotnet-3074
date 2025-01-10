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
        try
        {
            var (values, totalRecords) = await repository.GetAll(request);
            response.Result = values;
            response.TotalRecords = totalRecords;
            response.CurrentPage = request.PageNumber;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return response;
    }

    public async Task<ApiResponse<TEntity>> GetBySeq(GetBySeqRequest request)
    {
        var response = new ApiResponse<TEntity>();
        try
        {
            response.Result = await repository.GetById(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return response;
    }

    public async Task<ApiResponse<TEntity>> Create(CreateRequest<TEntity> request)
    {
        var response = new ApiResponse<TEntity>();
        try
        {
            response.Result = await repository.Create(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return response;
    }

    public async Task<ApiResponse<TEntity>> Update(UpdateRequest<TEntity> request)
    {
        var response = new ApiResponse<TEntity>();
        try
        {
            response.Result = await repository.Update(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return response;
    }

    public async Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request)
    {
        var response = new ApiResponse<bool>();
        try
        {
            response.Result = await repository.Delete(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return response;
    }
}
