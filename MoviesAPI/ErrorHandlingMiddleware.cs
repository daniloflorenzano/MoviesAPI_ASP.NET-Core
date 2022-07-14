using System.Net;
using System.Text.Json;

namespace MoviesAPI
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate Next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var code = HttpStatusCode.InternalServerError;

            if (error is ArgumentNullException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = error.Message }));
        }
    }
}
