using Microsoft.Extensions.Options;

namespace eVillageCinemas.Models
{
    public class AvailableDate
    {
        public int AvailableDateId { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public virtual ICollection<AvailableSeat> AvailableSeats { get; set; }
    }
}
