using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlRepositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        public Task<IEnumerable<User>> GetAll(UserParameters parameters);

    }
}