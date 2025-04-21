using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface IMovieService
    {
        public void CreateMovie(AdminViewModel adminViewModel);

        public void UpdateMovie(AdminViewModel adminViewModel);

        public void DeleteSingleMovie(int movieId);

        public void DeleteAllMovies();

        public Movie GetSingleMovie(int movieId);

        public List<Movie> GetAllMovies(int cinemaId = 0);

        public dynamic GetAllMoviesDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
