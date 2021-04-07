using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories.SQLRepositories
{
    public class TicketRepository : GenericRepository<Ticket, int>, ITicketRepository
    {
        public TicketRepository(EfConfig.MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Ticket>> GetAll(TicketParameters parameters)
        {
            return await _entities
                .Include(t => t.User)
                .Include(t => t.Route)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public new async Task<Ticket> GetOneById(int id)
        {
            return await _entities.Include(t => t.User).Include(t => t.Route).FirstAsync((t => t.Id.Equals(id)));
        }
    }
}