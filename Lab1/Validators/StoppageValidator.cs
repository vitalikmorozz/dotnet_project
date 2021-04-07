using FluentValidation;
using Lab1.DTOs.StoppageDTOs;

namespace Lab1.Validators
{
    public class StoppageValidator : AbstractValidator<StoppageRequestDto>
    {
        public StoppageValidator()
        {
            RuleFor(e => e.OrderNumber).NotEmpty();
            RuleFor(e => e.ArrivalTime).NotEmpty();
            RuleFor(e => e.DepartureTime).NotEmpty();
            RuleFor(e => e.RouteId).NotEmpty();
            RuleFor(e => e.RouteId).NotEmpty();
        }
    }
}