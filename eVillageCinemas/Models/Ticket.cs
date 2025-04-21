namespace eVillageCinemas.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int AvailableSeatId { get; set; }

        public virtual AvailableSeat AvailableSeat { get; set; }
    }
}
