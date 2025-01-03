namespace Reservation.Application.Models.Auth
{
    public record SignInDto
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Token { get; set; } = null!;

    }
}
