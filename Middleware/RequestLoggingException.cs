using System.Diagnostics;

namespace LinkVault.Middleware
{
    public class RequestLoggingMiddelware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddelware> _logger;

        public RequestLoggingMiddelware(RequestDelegate next, ILogger<RequestLoggingMiddelware> logger)
        {  

            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation("=> REQ {Method} {Path} {Quary}", context.Request.Method, context.Request.Path, context.Request.QueryString);
            await _next(context);
            stopWatch.Stop();
            _logger.LogInformation("<= RES {Method} {Path} responed {statusCode} in {time}", context.Request.Method, context.Request.Path, context.Response.StatusCode, stopWatch.ElapsedMilliseconds);
        }
    }
}