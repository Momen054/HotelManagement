using HotelManagement.DTOs.Review;

namespace HotelManagement.serviceInterfaces
{
    public interface IReview
    {
        Task<IEnumerable<GetReview>> GetReview();

        Task<GetReview> GetReview(int id);

        Task<IEnumerable<GetReview>> GetGuestReview(string id);

        Task PutReview(PutReview review);

        Task PostReview(PostReview review);

        Task DeleteReview(int id);

        Task DeleteGuestReview(int id,string userId);

    }
}
