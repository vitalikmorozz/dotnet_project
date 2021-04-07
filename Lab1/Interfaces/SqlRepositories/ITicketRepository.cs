using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface ITicketRepository : IGenericRepository<Ticket, int>
    {
        public Task<IEnumerable<Ticket>> GetAll(TicketParameters parameters);

    }
}