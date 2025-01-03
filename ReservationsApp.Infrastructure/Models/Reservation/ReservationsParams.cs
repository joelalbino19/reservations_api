namespace Reservation.Application.Models.Reservation
{
    public record ReservationsParams
    {
        public int? UserId { get; set; }
        public int? SpaceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
