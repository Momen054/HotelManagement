using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Reservation;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.Reservation
{
    public class PostReservationValidator : AbstractValidator<PostReservation>
    {
        public PostReservationValidator(HotelManagementContext context) 
        {
            RuleFor(p => p.No_Nights)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(p => p.GuestId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Guests.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Guest Id");
        }
    }
}
