namespace eVillageCinemas.Models
{
    public class Cinema
    {
        public int CinemaId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
