using HotelManagement.DTOs.Reservation;

namespace HotelManagement.serviceInterfaces
{
    public interface IReservation
    {
        Task<IEnumerable<GetReservation>> GetReservation();

        Task<GetReservation> GetReservation(int id);

        Task<IEnumerable<GetReservation>> GetGuestReservation(string id);

        Task PutReservation(PutReservation reservation);

        Task PutGuestReservation(PutReservation reservation, string id);

        Task PostReservation(PostReservation reservation);

        Task DeleteReservation(int id);

        Task DeleteGuestReservation(int id,string userId);

    }
}
