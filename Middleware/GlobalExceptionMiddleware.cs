//using LinkVault.Exceptions;
//using System.Net;

//namespace LinkVault.Middleware
//{
//    public class GlobalExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<GlobalExceptionMiddleware> _logger;
//        private HttpStatusCode stauesCode;

//        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }
//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                var (statusCode, message) = ex switch
//                {
//                    NotFoundException => (HttpStatusCode.NotExtended, ex.Message),
//                    BadRequestException => (HttpStatusCode.BadRequest, ex.Message),
//                    DuplicateException => (HttpStatusCode.Conflict, ex.Message),
//                    _ => (HttpStatusCode.InternalServerError, "Something went wrong!")
//                };

//                if (stauesCode == HttpStatusCode.InternalServerError)
//                    _logger.LogError("Exception: {Message}", ex.Message);
//                else
//                    _logger.LogWarning("Handeld Expection: {Type} - {Mesasge}", ex.GetType().Name, ex.Message);

//                context.Response.StatusCode = (int)stauesCode;
//                context.Response.ContentType = "application/json";

//                await context.Response.WriteAsJsonAsync(new
//                {
//                    status = (int)stauesCode,
//                    message,
//                });

//            }
//        }
//    }
//}

using LinkVault.Exceptions;
using System.Net;

namespace LinkVault.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

       
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted) throw;

                var (statusCode, message) = ex switch
                {
                    NotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    BadRequestException => (HttpStatusCode.BadRequest, ex.Message),
                    DuplicateException => (HttpStatusCode.Conflict, ex.Message),
                    _ => (HttpStatusCode.InternalServerError, ex.Message)  
                };

                if (statusCode == HttpStatusCode.InternalServerError)
                    _logger.LogError("Exception: {Message}", ex.Message);
                else
                    _logger.LogWarning("Handled Exception: {Type} - {Message}", ex.GetType().Name, ex.Message);

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    status = (int)statusCode,
                    message,
                });
            }
        }
    }
}