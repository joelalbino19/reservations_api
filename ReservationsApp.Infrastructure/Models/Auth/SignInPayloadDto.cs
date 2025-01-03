namespace Reservation.Application.Models.Auth
{
    public record SignInPayloadDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
