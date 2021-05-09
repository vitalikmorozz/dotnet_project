using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.TicketDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlServices;

namespace Lab1.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Ticket>> GetAll(TicketParameters ticketParameters)
        {
            return await _unitOfWork._ticketRepository.GetAll(ticketParameters);
        }

        public async Task<Ticket> GetOneById(int id)
        {
            return await _unitOfWork._ticketRepository.GetOneById(id);
        }

        public async Task<Ticket> Create(TicketRequestDto dto)
        {
            Ticket entity = _mapper.Map<TicketRequestDto, Ticket>(dto);
            entity.Route = await _unitOfWork._routeRepository.GetOneById(dto.RouteId);
            
            Ticket ticket = await _unitOfWork._ticketRepository.Create(entity);
            await _unitOfWork.SaveChanges();
            return ticket;
        }

        public async Task<int> DeleteById(int id)
        {
            if (!await _unitOfWork._ticketRepository.ExistsById(id))
                throw new HttpRequestException("Entity with specified id not found", null, HttpStatusCode.NotFound);
            
            int byId = await _unitOfWork._ticketRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            return byId;
        }

        public async Task<Ticket> Update(int id, TicketRequestDto dto)
        {
            Ticket entity = _mapper.Map<TicketRequestDto, Ticket>(dto);
            entity.Id = id;
            entity.Route = await _unitOfWork._routeRepository.GetOneById(dto.RouteId);

            Ticket ticket = await _unitOfWork._ticketRepository.Update(entity);
            await _unitOfWork.SaveChanges();
            return ticket;
        }
    }
}