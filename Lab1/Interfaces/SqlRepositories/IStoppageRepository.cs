using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface IStoppageRepository : IGenericRepository<Stoppage, int>
    {
        public Task<IEnumerable<Stoppage>> GetAll(StoppageParameters parameters);

    }
}