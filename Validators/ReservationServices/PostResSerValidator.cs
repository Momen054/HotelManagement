using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.ReservationServices;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.ReservationServices
{
    public class PostResSerValidator : AbstractValidator<PostResSer>
    {
        public PostResSerValidator(HotelManagementContext context)
        {
            RuleFor(p => p.ReservationId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Reservations.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Reservation Id");

            RuleFor(p => p.ServiceId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Services.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Service Id");
        }
    }
}
