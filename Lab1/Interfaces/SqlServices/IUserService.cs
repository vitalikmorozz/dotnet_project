using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.DTOs.UserDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(UserParameters userParameters);

        Task<User> GetOneById(int id);

        Task<User> Create(UserRequestDto entity);

        Task<int> DeleteById(int id);

        Task<User> Update(int id, UserRequestDto entity);
    }
}