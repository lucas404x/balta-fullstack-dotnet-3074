using Dima.Core.Entities;
using Dima.Core.Requests;

namespace Dima.Api.Domain.Abstractions;

public interface IEntityRepository<T> where T : BaseEntity
{
    Task<(List<T> values, int totalRecords)> GetAll(GetAllRequest<T> request);
    Task<T?> GetById(GetBySeqRequest request);
    Task<T> Create(CreateRequest<T> request);
    Task<T?> Update(UpdateRequest<T> request);
    Task<bool?> Delete(DeleteBySeqRequest request);
}