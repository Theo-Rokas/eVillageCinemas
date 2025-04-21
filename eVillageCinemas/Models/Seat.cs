namespace eVillageCinemas.Models
{
    public class Seat
    {
        public int SeatId { get; set; }

        public string? Code { get; set; }

        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }
    }
}
