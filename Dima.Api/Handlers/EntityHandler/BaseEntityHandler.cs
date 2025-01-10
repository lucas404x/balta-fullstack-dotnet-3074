using Dima.Api.Data.Repositories;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Handlers.EntityHandler;

abstract public class BaseEntityHandler<T>(IEntityRepository<T> repository) : IEntityHandler<T> where T : BaseEntity
{
    public async Task<PagedApiResponse<List<T>>> GetAll(GetAllRequest<T> request)
    {
        var response = new PagedApiResponse<List<T>>();
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

    public async Task<ApiResponse<T>> GetBySeq(GetBySeqRequest request)
    {
        var response = new ApiResponse<T>();
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

    public async Task<ApiResponse<T>> Create(CreateRequest<T> request)
    {
        var response = new ApiResponse<T>();
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

    public async Task<ApiResponse<T>> Update(UpdateRequest<T> request)
    {
        var response = new ApiResponse<T>();
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
