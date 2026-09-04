using FluentValidation;
using HotelManagement.DTOs.RoomType;

namespace HotelManagement.Validators.RoomType
{
    public class PostRoomTypeValidator : AbstractValidator<PostRoomType>
    {
        public PostRoomTypeValidator() 
        {
            RuleFor(p => p.Name)
                .MinimumLength(3)
                .NotEmpty();

            RuleFor(p => p.Capacity)
                .GreaterThanOrEqualTo(1)
                .NotEmpty();

            RuleFor(p => p.PricePerNight)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
