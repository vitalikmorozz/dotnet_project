using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface ITrainRepository : IGenericRepository<Train, int>
    {
        public Task<IEnumerable<Train>> GetAll(TrainParameters parameters);

    }
}