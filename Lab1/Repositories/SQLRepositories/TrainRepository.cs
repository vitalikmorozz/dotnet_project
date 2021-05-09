using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories.SQLRepositories
{
    public class TrainRepository : GenericRepository<Train, int>, ITrainRepository
    {
        public TrainRepository(EfConfig.MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Train>> GetAll(TrainParameters parameters)
        {
            return await _entities
                .Include(t => t.Routes)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public new async Task<Train> GetOneById(int id)
        {
            return await _entities.Include(t => t.Routes).FirstAsync((t => t.Id.Equals(id)));
        }
    }
}