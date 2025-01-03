namespace Reservation.Application.Models.Space
{
    public record SpaceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int? Capacity { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
