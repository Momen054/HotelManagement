using System.Diagnostics;

namespace HotelManagement.Middelware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation("Request Started => {Method} {Path}", context.Request.Method, context.Request.Path);
            try
            {
                await _next(context);
            }
            finally
            {
                watch.Stop();
                _logger.LogInformation("Request Finished => {Method} {Path} StatusCode:{StatusCode} Time:{Elapsed} ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    watch.ElapsedMilliseconds);
            }
        }
    }
}
