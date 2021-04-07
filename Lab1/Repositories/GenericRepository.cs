using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories
{
    public abstract class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId>
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _entities;

        public GenericRepository(EfConfig.MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(QueryStringParameters parameters)
        {
            return await _entities.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize)
                .ToListAsync();
        }

        public async Task<TEntity> GetOneById(TId id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<TId> DeleteById(TId id)
        {
            TEntity entity = await _entities.FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            return id;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _entities.AnyAsync(entity => entity.Id.Equals(id));
        }
    }
}