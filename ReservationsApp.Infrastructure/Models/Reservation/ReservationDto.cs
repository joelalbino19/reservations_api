namespace Reservation.Application.Models.Reservation
{
    public record ReservationDto
    {
        public int Id { get; set; }

        public string SpaceName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;
    }
}