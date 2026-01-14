using JobTracker.Application.Common;
using System.Net;
using System.Text.Json;

namespace JobTracker.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next; 

        public GlobalExceptionMiddleware(RequestDelegate next   )
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
              private static Task HandleException(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var response = ApiResponse<string>.Fail(
                ex.Message,
                new List<string> { ex.GetType().Name }
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
