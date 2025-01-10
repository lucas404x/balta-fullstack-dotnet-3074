using Dima.Api.Domain.Abstractions;
using Dima.Api.Domain.Exceptions.EntityExceptions;
using Dima.Core.Entities;
using Dima.Core.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Text;

namespace Dima.Api.Data.Repositories;

public class EntityRepository<TEntity>(AppDbContext dbContext) : IEntityRepository<TEntity> 
    where TEntity : BaseEntity
{
    private static readonly FrozenSet<string> _entityProps = typeof(TEntity).GetProperties()
        .Select(x => x.Name.ToLower())
        .ToFrozenSet();

    private static readonly (List<TEntity> values, int totalRecords) _emptyRecords = ([], 0);

    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    
    public async Task<(List<TEntity> values, int totalRecords)> GetAll(GetAllRequest<TEntity> request)
    {
        StringBuilder queryBuilder = new();

        var userIdParam = new SqlParameter("UserId", request.UserId);
        queryBuilder.Append($"SELECT * FROM [{request.TableName}] WHERE UserId = @UserId");
        
        int totalRecords = await _dbSet.
            FromSqlRaw(queryBuilder.ToString(), userIdParam).
            CountAsync();

        if (totalRecords == 0) return _emptyRecords;
        if (request.OrderByProperties?.Count > 0)
        {
            ValidateOrderByProperties(request);
            ApplyOrderBy(queryBuilder, request.OrderByProperties);
        }
        queryBuilder.Append($" OFFSET {request.PageSize} * {request.PageNumber - 1} ROWS FETCH NEXT {request.PageSize} ROWS ONLY;");

        var result = await _dbSet.
            FromSqlRaw(queryBuilder.ToString(), userIdParam).
            AsNoTracking().
            ToListAsync();

        return (result, totalRecords);
    }

    private static void ValidateOrderByProperties(GetAllRequest<TEntity> request)
    {
        foreach (var orderByProp in request.OrderByProperties!)
        {
            if (!_entityProps.Contains(orderByProp.Property))
            {
                throw new InvalidProvidedColumnNameException(request.TableName, orderByProp.Property);
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

    public async Task<TEntity?> GetById(GetBySeqRequest request)
        => request.Seq == 0 
        ? null 
        : await _dbSet.FirstOrDefaultAsync(x => x.Seq == request.Seq && request.UserId == x.UserId);

    public async Task<TEntity> Create(CreateRequest<TEntity> request)
    {
        await _dbSet.AddAsync(request.Entity);
        await dbContext.SaveChangesAsync();
        return request.Entity;
    }

    public async Task<TEntity?> Update(UpdateRequest<TEntity> request)
    {
        if (request.Entity.Seq == 0) return null;
        bool entityExists = await _dbSet.AnyAsync(x => x.Seq == request.Entity.Seq && x.UserId == request.Entity.UserId);
        if (!entityExists) return null;
        _dbSet.Update(request.Entity);
        await dbContext.SaveChangesAsync();
        return request.Entity;
    }

    public async Task<bool?> Delete(DeleteBySeqRequest request)
    {
        if (request.Seq == 0) return null;
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Seq == request.Seq);
        if (entity is null) return null;
        _dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
