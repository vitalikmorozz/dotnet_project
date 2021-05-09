using System;
using FluentValidation;
using Lab1.DTOs.TrainDTOs;

namespace Lab1.Validators
{
    public class TrainValidator : AbstractValidator<TrainRequestDto>
    {
        public TrainValidator()
        {
            RuleFor(e => e.TrainNumber).NotEmpty();
            RuleFor(e => e.Description).NotEmpty().Length(5, Int32.MaxValue);
            RuleFor(e => e.SeatNumber).NotEmpty();
        }
    }
}