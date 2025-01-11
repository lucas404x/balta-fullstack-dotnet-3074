using Dima.Core.Entities;
using Dima.Core.Requests;

namespace Dima.Api.Domain.Abstractions;

public interface IEntityRepository<T> where T : BaseEntity
{
    Task<(List<T> values, int totalRecords)> GetAll(GetAllRequest<T> request, CancellationToken cancellationToken = default);
    Task<T?> GetById(GetBySeqRequest request, CancellationToken cancellationToken = default);
    Task<T> Create(CreateRequest<T> request, CancellationToken cancellationToken = default);
    Task<T?> Update(UpdateRequest<T> request, CancellationToken cancellationToken = default);
    Task<bool?> Delete(DeleteBySeqRequest request, CancellationToken cancellationToken = default);
}