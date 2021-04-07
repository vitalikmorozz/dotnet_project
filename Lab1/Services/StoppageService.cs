using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.StoppageDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class StoppageService : IStoppageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoppageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Stoppage>> GetAll(StoppageParameters stoppageParameters)
        {
            return await _unitOfWork._stoppageRepository.GetAll(stoppageParameters);
        }

        public async Task<Stoppage> GetOneById(int id)
        {
            return await _unitOfWork._stoppageRepository.GetOneById(id);
        }

        public async Task<Stoppage> Create(StoppageRequestDto dto)
        {
            Stoppage entity = _mapper.Map<StoppageRequestDto, Stoppage>(dto);
            entity.Station = await _unitOfWork._stationRepository.GetOneById(dto.StationId);
            entity.StationId = dto.StationId;
            
            Stoppage stoppage = await _unitOfWork._stoppageRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return stoppage;
        }

        public async Task<int> DeleteById(int id)
        {
            if (!await _unitOfWork._stoppageRepository.ExistsById(id))
                throw new HttpRequestException("Entity with specified id not found", null, HttpStatusCode.NotFound);
            
            int byId = await _unitOfWork._stoppageRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<Stoppage> Update(int id, StoppageRequestDto dto)
        {
            Stoppage entity = _mapper.Map<StoppageRequestDto, Stoppage>(dto);
            entity.Id = id;
            entity.Station = await _unitOfWork._stationRepository.GetOneById(dto.StationId);
            entity.StationId = dto.StationId;
            
            Stoppage stoppage = await _unitOfWork._stoppageRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return stoppage;
        }
    }
}