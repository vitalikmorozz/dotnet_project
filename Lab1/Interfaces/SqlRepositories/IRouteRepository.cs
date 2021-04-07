using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface IRouteRepository : IGenericRepository<Route, int>
    {
        public Task<IEnumerable<Route>> GetAll(RouteParameters parameters);

    }
}