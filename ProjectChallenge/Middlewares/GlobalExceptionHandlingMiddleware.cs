using Newtonsoft.Json;
using ProjectChallengeDomain.Models;
using System.Net;

namespace ProjectChallengeAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }
        public Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ExceptionHandling details = new ExceptionHandling
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Internal Server Error",
                Detail = ex.Message,
            };
            string json = JsonConvert.SerializeObject(details);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);

        }
    }
}
