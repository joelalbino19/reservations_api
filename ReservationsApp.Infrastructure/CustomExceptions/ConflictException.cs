namespace Reservation.Application.CustomExceptions
{
    public class ConflictException : Exception
    {
        public string? Property { get; set; }

        public ConflictException(string message, string? propertyName) : base(message)
        {
            Property = propertyName;
        }
    }
}
