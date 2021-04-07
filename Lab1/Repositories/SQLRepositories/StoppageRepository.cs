using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories.SQLRepositories
{
    public class StoppageRepository : GenericRepository<Stoppage, int>, IStoppageRepository
    {
        public StoppageRepository(EfConfig.MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Stoppage>> GetAll(StoppageParameters parameters)
        {
            return await _entities
                .Include(s => s.Station)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public new async Task<Stoppage> GetOneById(int id)
        {
            return await _entities.Include(s => s.Station).FirstAsync((s => s.Id.Equals(id)));
        }
    }
}