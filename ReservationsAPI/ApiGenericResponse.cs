using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ReservationsAPI
{
    public class ApiGenericResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static IActionResult GenericResponse(T? data = default, bool status = true, int statusCode = 200, string message = "Process completed successfully")
        {
            var response = new ApiGenericResponse<T>
            {
                Success = status,
                Data = data,
                StatusCode = statusCode,
                Message = message
            };
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static IActionResult GenericResponseVoid(bool status = true, int statusCode = 200, string message = "Process completed successfully")
        {
            var response = new ApiGenericResponse<object>
            {
                Success = status,
                Data = default,
                StatusCode = statusCode,
                Message = message
            };
            return new ObjectResult(response) { StatusCode = statusCode };
        }
    }
}
