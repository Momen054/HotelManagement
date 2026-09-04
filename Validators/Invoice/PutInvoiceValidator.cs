using HotelManagement.Data;

namespace HotelManagement.Validators.Invoice
{
    public class PutInvoiceValidator : PostInvoiceValidator
    {
        public PutInvoiceValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
