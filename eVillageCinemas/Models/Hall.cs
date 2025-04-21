namespace eVillageCinemas.Models
{
    public class Hall
    {
        public int HallId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<AvailableDate> AvailableDates { get; set; }
    }
}
