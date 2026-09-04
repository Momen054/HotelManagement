using HotelManagement.Data;

namespace HotelManagement.Validators.Review
{
    public class PutReviewValidator : PostReviewValidator
    {
        public PutReviewValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
