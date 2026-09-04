namespace HotelManagement.DTOs.UserRole
{
    public class PostUserRole
    {
        public string UserId { get; set; }

        public IList<string> Roles { get; set; }
    }
}
