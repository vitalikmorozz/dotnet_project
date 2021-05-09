using FluentValidation;
using Lab1.DTOs.StationDTOs;
using Lab1.Entities;

namespace Lab1.Validators
{
    public class StationValidator : AbstractValidator<StationRequestDto>
    {
        public StationValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
        }
    }
}