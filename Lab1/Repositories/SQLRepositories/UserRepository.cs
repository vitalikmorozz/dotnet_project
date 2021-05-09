using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Repositories.SQLRepositories
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(EfConfig.MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAll(UserParameters parameters)
        {
            var list = await _entities
                .Include(u => u.Tickets)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            if (!parameters.FirstNameSearch.Equals(String.Empty))
            {
                list = list.FindAll((user => user.FirstName.Contains(parameters.FirstNameSearch)));
            }
            
            if (!parameters.LastNameSearch.Equals(String.Empty))
            {
                list = list.FindAll((user => user.LastName.Contains(parameters.LastNameSearch)));
            }
            
            return list;
        }

        public new async Task<User> GetOneById(int id)
        {
            return await _entities.Include(u => u.Tickets).FirstAsync((u => u.Id.Equals(id)));
        }
    }
}