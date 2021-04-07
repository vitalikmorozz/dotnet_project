using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.TrainDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class TrainService : ITrainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Train>> GetAll(TrainParameters trainParameters)
        {
            return await _unitOfWork._trainRepository.GetAll(trainParameters);
        }

        public async Task<Train> GetOneById(int id)
        {
            return await _unitOfWork._trainRepository.GetOneById(id);
        }

        public async Task<Train> Create(TrainRequestDto dto)
        {
            Train entity = _mapper.Map<TrainRequestDto, Train>(dto);
            Train train = await _unitOfWork._trainRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return train;
        }

        public async Task<int> DeleteById(int id)
        {
            int byId = await _unitOfWork._trainRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<Train> Update(int id, TrainRequestDto dto)
        {
            Train entity = _mapper.Map<TrainRequestDto, Train>(dto);
            entity.Id = id;
            Train train = await _unitOfWork._trainRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return train;
        }
    }
}