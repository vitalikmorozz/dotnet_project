using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.StationDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class StationService : IStationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Station>> GetAll(StationParameters stationParameters)
        {
            return await _unitOfWork._stationRepository.GetAll(stationParameters);
        }

        public async Task<Station> GetOneById(int id)
        {
            return await _unitOfWork._stationRepository.GetOneById(id);
        }

        public async Task<Station> Create(StationRequestDto dto)
        {
            Station entity = _mapper.Map<StationRequestDto, Station>(dto);
            Station station = await _unitOfWork._stationRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return station;
        }

        public async Task<int> DeleteById(int id)
        {
            if (!await _unitOfWork._stationRepository.ExistsById(id))
                throw new HttpRequestException("Entity with specified id not found", null, HttpStatusCode.NotFound);
            
            int byId = await _unitOfWork._stationRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<Station> Update(int id, StationRequestDto dto)
        {
            Station entity = _mapper.Map<StationRequestDto, Station>(dto);
            entity.Id = id;
            Station station = await _unitOfWork._stationRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return station;
        }
    }
}