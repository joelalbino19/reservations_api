namespace Reservation.Application.Models.Util
{
    public record SpaceSelectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
