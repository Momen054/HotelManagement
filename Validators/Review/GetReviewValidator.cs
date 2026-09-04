using HotelManagement.Data;

namespace HotelManagement.Validators.Review
{
    public class GetReviewValidator : PutReviewValidator
    {
        public GetReviewValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
