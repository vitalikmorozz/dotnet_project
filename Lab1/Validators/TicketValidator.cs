using FluentValidation;
using Lab1.DTOs.TicketDTOs;

namespace Lab1.Validators
{
    public class TicketValidator : AbstractValidator<TicketRequestDto>
    {
        public TicketValidator()
        {
            RuleFor(e => e.RouteId).NotEmpty();
        }
    }
}