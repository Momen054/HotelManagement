using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Invoice;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.Invoice
{
    public class PostInvoiceValidator : AbstractValidator<PostInvoice>
    {
        public PostInvoiceValidator(HotelManagementContext context)
        {
            RuleFor(p => p.Tax)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();

            RuleFor(p => p.ReservationId)
                .NotEmpty()
                .GreaterThan(0)
                .MustAsync(async (id, ct) =>
                    await context.Reservations.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("invalid Reservation Id");

        }
    }
}
