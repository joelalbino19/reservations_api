namespace Reservation.Application.Models.Space
{
    public record CreateSpaceDto
    {
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int? Capacity { get; set; }
    }
}
