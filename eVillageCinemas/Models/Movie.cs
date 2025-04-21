namespace eVillageCinemas.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string? Title { get; set; }

        public string? Type { get; set; }

        public decimal? Price { get; set; }

        public int Duration { get; set; }

        public string? Description { get; set; }

        public string? Actors { get; set; }

        public string? Image { get; set; }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public virtual ICollection<AvailableDate> AvailableDates { get; set; }
    }
}
