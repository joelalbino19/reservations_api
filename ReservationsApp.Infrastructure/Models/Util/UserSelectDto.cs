
namespace Reservation.Application.Models.Util
{
    public record UserSelectDto
    {
        public int Id { get;set; }
        public string Name { get; set; } = null!;
    }
}
