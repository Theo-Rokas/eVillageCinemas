namespace eVillageCinemas.Models
{
    public class AvailableSeat
    {
        public int AvailableSeatId { get; set; }

        public string? Code { get; set; }

        public bool IsAvailable { get; set; }

        public int AvailableDateId { get; set; }

        public virtual AvailableDate AvailableDate { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
