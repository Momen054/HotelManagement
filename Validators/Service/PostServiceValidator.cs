using FluentValidation;
using HotelManagement.DTOs.Service;

namespace HotelManagement.Validators.Service
{
    public class PostServiceValidator : AbstractValidator<PostService>
    {
        public PostServiceValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(p => p.UnitPrice)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(p => p.Quantity)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
