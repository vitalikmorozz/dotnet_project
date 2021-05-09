using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities.Parameters;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface IGenericRepository<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        Task<IEnumerable<TEntity>> GetAll(QueryStringParameters parameters);

        Task<TEntity> GetOneById(TId id);

        Task<TEntity> Create(TEntity entity);

        Task<TId> DeleteById(TId id);

        Task<TEntity> Update(TEntity entity);

        Task<bool> ExistsById(int id);
    }
}