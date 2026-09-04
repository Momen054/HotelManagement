namespace HotelManagement.Middelware
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public MaintenanceMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var isMaintenance = _configuration.GetValue<bool>("Maintenance:Enabled");
            if (isMaintenance)
            {
                throw new Exception("The server is under maintenance. Please try again later.");

            }

            await _next(context);
        }
    }
}
