using FluentValidation;
using Lab1.DTOs.RouteDTOs;

namespace Lab1.Validators
{
    public class RouteValidator : AbstractValidator<RouteRequestDto>
    {
        public RouteValidator()
        {
            RuleFor(e => e.TrainId).NotEmpty();
        }
    }
}