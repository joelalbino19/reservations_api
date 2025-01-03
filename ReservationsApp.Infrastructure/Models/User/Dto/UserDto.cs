namespace Reservation.Application.Models.User.Dto
{
    public record UserDto
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
