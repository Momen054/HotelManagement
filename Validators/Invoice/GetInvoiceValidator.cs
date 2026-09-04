using FluentValidation;
using HotelManagement.Data;
using HotelManagement.DTOs.Invoice;

namespace HotelManagement.Validators.Invoice
{
    public class GetInvoiceValidator : PutInvoiceValidator
    {
        public GetInvoiceValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
