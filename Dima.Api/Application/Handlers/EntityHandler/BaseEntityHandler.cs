﻿using Dima.Api.Domain.Abstractions;
using Dima.Api.Domain.Exceptions.EntityExceptions;
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
        var (values, totalRecords) = await repository.GetAll(request);
        return new(values, "Registros recuperados com sucesso!", request.PageNumber, totalRecords, request.PageSize);
    }

    public async Task<ApiResponse<TEntity>> GetBySeq(GetBySeqRequest request)
    {
        var result = await repository.GetById(request);
        return result
            is not null ? new(result, $"Registro {request.Seq} recuperado com sucesso!")
            : throw new EntityNotFoundException(request.Seq);
    }

    public async Task<ApiResponse<TEntity>> Create(CreateRequest<TEntity> request)
        => new(await repository.Create(request), "Registro criado com sucesso!");

    public async Task<ApiResponse<TEntity>> Update(UpdateRequest<TEntity> request)
    {
        var result = await repository.Update(request);
        return result
            is not null ? new(result, "Registro atualizado com sucesso!")
            : throw new EntityNotFoundException(request.Entity.Seq);
    }

    public async Task<ApiResponse<bool>> Delete(DeleteBySeqRequest request)
    {
        var result = await repository.Delete(request);
        return result
            is not null ? new(result ?? false, $"Registro {request.Seq} removido com sucesso!")
            : throw new EntityNotFoundException(request.Seq);
    }
}
