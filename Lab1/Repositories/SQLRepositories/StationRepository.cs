using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories.SQLRepositories
{
    public class StationRepository : GenericRepository<Station, int>, IStationRepository
    {
        public StationRepository(EfConfig.MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Station>> GetAll(StationParameters parameters)
        {
            return await _entities
                .Include(s => s.Stoppages)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public new async Task<Station> GetOneById(int id)
        {
            return await _entities.Include(s => s.Stoppages).FirstAsync((s => s.Id.Equals(id)));
        }
    }
}