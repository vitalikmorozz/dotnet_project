using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.DTOs.TicketDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;

namespace Lab1.Interfaces.SqlServices
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAll(TicketParameters ticketParameters);

        Task<Ticket> GetOneById(int id);

        Task<Ticket> Create(TicketRequestDto entity);

        Task<int> DeleteById(int id);

        Task<Ticket> Update(int id, TicketRequestDto entity);
    }
}