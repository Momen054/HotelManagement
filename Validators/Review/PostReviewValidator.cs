using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Review;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.Review
{
    public class PostReviewValidator : AbstractValidator<PostReview>
    {
        public PostReviewValidator(HotelManagementContext context)
        {
            RuleFor(p => p.ReservationId)
                .NotEmpty()
                .MustAsync(async (id, ct) =>
                    await context.Reservations.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("Invalid Reservation Id");

            RuleFor(p => p.GuestId)
                .NotEmpty()
                .MustAsync(async (id,ct) =>
                    await context.Guests.AnyAsync(p => p.Id == id,ct)
                ).WithMessage("Invalid Guest Id");

            RuleFor(p=>p.Comment)
                .MinimumLength(5)
                .NotEmpty();

            RuleFor(p => p.Rating)
                .GreaterThan(0)
                .LessThanOrEqualTo(5)
                .NotEmpty();
                               
        }
    }
}
