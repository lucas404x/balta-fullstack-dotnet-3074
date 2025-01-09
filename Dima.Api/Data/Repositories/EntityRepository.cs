using Azure.Core;
using Dima.Core.Entities;
using Dima.Core.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Text;

namespace Dima.Api.Data.Repositories;

public interface IEntityRepository<T> where T : BaseEntity
{
    Task<(List<T> values, int totalRecords)> GetAll(GetAllRequest<T> request);
    Task<T?> GetById(GetBySeqRequest request);
    Task<T> Create(CreateRequest<T> request);
    Task<T> Update(UpdateRequest<T> request);
    Task<bool> Delete(DeleteBySeqRequest request);
}

public class EntityRepository<T>(AppDbContext dbContext) : IEntityRepository<T> where T : BaseEntity
{
    private static readonly FrozenSet<string> _entityProps = typeof(T).GetProperties()
        .Select(x => x.Name.ToLower())
        .ToFrozenSet();

    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public async Task<(List<T> values, int totalRecords)> GetAll(GetAllRequest<T> request)
    {
        StringBuilder queryBuilder = new();

        var userIdParam = new SqlParameter("UserId", request.UserId);
        queryBuilder.Append($"SELECT * FROM {request.ColumnName} WHERE UserId = @UserId");
        
        int totalRecords = await _dbSet.
            FromSqlRaw(queryBuilder.ToString(), userIdParam).
            CountAsync();

        if (totalRecords == 0) return ([], 0);
        if (request.OrderByProperties?.Count > 0)
        {
            ValidateOrderByProperties(request.OrderByProperties);
            ApplyOrderBy(queryBuilder, request.OrderByProperties);
        }
        queryBuilder.Append($" OFFSET {request.PageSize} * {request.PageNumber - 1} ROWS FETCH NEXT {request.PageSize} ROWS ONLY;");

        var result = await _dbSet.
            FromSqlRaw(queryBuilder.ToString(), userIdParam).
            AsNoTracking().
            ToListAsync();

        return (result, totalRecords);
    }

    private static void ValidateOrderByProperties(List<RequestOrderByProp> orderByProperties)
    {
        foreach (var orderByProp in orderByProperties)
        {
            if (!_entityProps.Contains(orderByProp.Property))
            {
                throw new ArgumentException($"Property {orderByProp.Property} does not exist in entity {typeof(T).Name}");
            }
        }
    }

    private static void ApplyOrderBy(StringBuilder queryBuilder, List<RequestOrderByProp> orderByProperties)
    {
        queryBuilder.Append(" ORDER BY ");
        for (int i = 0; i < orderByProperties.Count; i++)
        {
            var orderByProp = orderByProperties[i];
            queryBuilder.Append($"{orderByProp.Property} {(orderByProp.Ascending ? "ASC" : "DESC")}");
            if (i < orderByProperties.Count - 1)
            {
                queryBuilder.Append(", ");
            }
        }
    }

    public async Task<T?> GetById(GetBySeqRequest request)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Seq == request.Seq);
    }

    public async Task<T> Create(CreateRequest<T> request)
    {
        await _dbSet.AddAsync(request.Entity);
        await dbContext.SaveChangesAsync();
        return request.Entity;
    }

    public async Task<T> Update(UpdateRequest<T> request)
    {
        _dbSet.Update(request.Entity);
        await dbContext.SaveChangesAsync();
        return request.Entity;
    }

    public async Task<bool> Delete(DeleteBySeqRequest request)
    {
        int affectedRows = await _dbSet.
            Where(x => x.Seq == request.Seq).
            ExecuteDeleteAsync();
        return affectedRows == 1;
    }
}
