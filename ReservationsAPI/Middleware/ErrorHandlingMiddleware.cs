using Microsoft.AspNetCore.Mvc;
using Reservation.Application.CustomExceptions;
using System.Net;

namespace ReservationsAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                ConflictException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = ApiGenericResponse<object?>.GenericResponse(
                data: null,
                status: false,
                statusCode: statusCode,
                message: exception.Message
            );

            var objectResult = (ObjectResult)response;
            var responseObject = objectResult.Value as ApiGenericResponse<object>;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseObject));
        }
    }
}
