namespace HotelManagement.Options
{
    public class JwtOption
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SigningKey { get; set; }

        public int AccessTokenMinutes { get; set; }

        public int RefreshTokenDays { get; set; }

    }
}
