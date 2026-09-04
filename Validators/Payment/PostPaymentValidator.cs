using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Payment;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Validators.Payment
{
    public class PostPaymentValidator : AbstractValidator<PostPayment>
    {
        public PostPaymentValidator(HotelManagementContext context)
        {
            RuleFor(p => p.Status)
                .NotNull();

            RuleFor(p => p.InvoiceId)
                .NotEmpty()
                .GreaterThan(0)
                .MustAsync(async (id, ct) =>
                    await context.Invoices.AnyAsync(p => p.Id == id, ct)
                ).WithMessage("invalid Invoice Id");

        }
    }
}
