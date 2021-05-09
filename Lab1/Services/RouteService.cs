using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.RouteDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Route>> GetAll(RouteParameters routeParameters)
        {
            return await _unitOfWork._routeRepository.GetAll(routeParameters);
        }

        public async Task<Route> GetOneById(int id)
        {
            return await _unitOfWork._routeRepository.GetOneById(id);
        }

        public async Task<Route> Create(RouteRequestDto dto)
        {
            Route entity = _mapper.Map<RouteRequestDto, Route>(dto);
            entity.Train = await _unitOfWork._trainRepository.GetOneById(dto.TrainId);

            Route route = await _unitOfWork._routeRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return route;
        }

        public async Task<int> DeleteById(int id)
        {
            if (!await _unitOfWork._routeRepository.ExistsById(id))
                throw new HttpRequestException("Entity with specified id not found", null, HttpStatusCode.NotFound);
            
            int byId = await _unitOfWork._routeRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<Route> Update(int id, RouteRequestDto dto)
        {
            Route entity = _mapper.Map<RouteRequestDto, Route>(dto);
            entity.Id = id;
            entity.Train = await _unitOfWork._trainRepository.GetOneById(dto.TrainId);

            Route route = await _unitOfWork._routeRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return route;
        }
    }
}