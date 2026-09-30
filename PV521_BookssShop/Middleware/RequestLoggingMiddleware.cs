using System.Diagnostics;

namespace PV521_BookssShop.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Запит прийшов: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation(
                "Відповідь відправлена: {StatusCode}. Час виконання: {ElapsedMilliseconds} мс",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}