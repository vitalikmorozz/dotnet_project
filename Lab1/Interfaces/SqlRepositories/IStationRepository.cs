using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface IStationRepository : IGenericRepository<Station, int>
    {
        public Task<IEnumerable<Station>> GetAll(StationParameters parameters);

    }
}