using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.UserDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<User>> GetAll(UserParameters userParameters)
        {
            return await _unitOfWork._userRepository.GetAll(userParameters);
        }

        public async Task<User> GetOneById(int id)
        {
            return await _unitOfWork._userRepository.GetOneById(id);
        }

        public async Task<User> Create(UserRequestDto dto)
        {
            User entity = _mapper.Map<UserRequestDto, User>(dto);
            User User = await _unitOfWork._userRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return User;
        }

        public async Task<int> DeleteById(int id)
        {
            int byId = await _unitOfWork._userRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<User> Update(int id, UserRequestDto dto)
        {
            User entity = _mapper.Map<UserRequestDto, User>(dto);
            entity.Id = id;
            User User = await _unitOfWork._userRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return User;
        }
    }
}