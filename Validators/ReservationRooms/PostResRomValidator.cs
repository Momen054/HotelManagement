using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.ReservationRooms;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.ReservationRooms
{
    public class PostResRomValidator : AbstractValidator<PostResRom>
    {
        public PostResRomValidator(HotelManagementContext context) 
        {
            RuleFor(p => p.ReservationId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Reservations.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Reservation Id");

            RuleFor(p => p.RoomId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Rooms.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Room Id");
        }
    }
}
