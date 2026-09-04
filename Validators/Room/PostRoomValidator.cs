using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Room;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.Room
{
    public class PostRoomValidator : AbstractValidator<PostRoom>
    {
        public PostRoomValidator(HotelManagementContext context) 
        {
            RuleFor(p => p.RoomTypeId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.RoomTypes.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid RoomType Id");

            RuleFor(p=>p.Floor)
                .NotEmpty();

            RuleFor(p => p.Status)
                .MinimumLength(2)
                .NotEmpty();
        }
    }
}
