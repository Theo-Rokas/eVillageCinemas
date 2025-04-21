using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext _eVCDB;

        private readonly IHelperService _helperService;

        public MovieService(DatabaseContext eVDB, IHelperService helperService)
        {
            _eVCDB = eVDB;
            _helperService = helperService;
        }

        public void CreateMovie(AdminViewModel adminViewModel)
        {
            var fileUrl = _helperService.UploadFileAsync(adminViewModel.File).Result;

            for (int i = 0; i < adminViewModel.MultipleNumber; i++)
            {
                var movie = new Movie()
                {
                    Title = adminViewModel.Movie?.Title?.Trim(),
                    Type = adminViewModel.Movie?.Type?.Trim(),
                    Price = adminViewModel.Movie.Price,
                    Duration = adminViewModel.Movie.Duration,
                    Description = adminViewModel.Movie?.Description?.Trim(),
                    Actors = adminViewModel.Movie?.Actors?.Trim(),                    
                    CinemaId = adminViewModel.Cinema.CinemaId,
                };

                if (!string.IsNullOrWhiteSpace(fileUrl))
                    movie.Image = fileUrl.Trim();

                _eVCDB.Movies.Add(movie);
            }
            _eVCDB.SaveChanges();
        }

        public void UpdateMovie(AdminViewModel adminViewModel)
        {
            var fileUrl = adminViewModel.File != null ? _helperService.UploadFileAsync(adminViewModel.File).Result : "";

            var movie = GetSingleMovie(adminViewModel.Movie.MovieId);
            movie.Title = adminViewModel.Movie?.Title?.Trim();
            movie.Type = adminViewModel.Movie?.Type?.Trim();
            movie.Price = adminViewModel.Movie.Price;
            movie.Duration = adminViewModel.Movie.Duration;
            movie.Description = adminViewModel.Movie?.Description?.Trim();
            movie.Actors = adminViewModel.Movie?.Actors?.Trim();           
            movie.CinemaId = adminViewModel.Cinema.CinemaId;

            if (!string.IsNullOrWhiteSpace(fileUrl))
                movie.Image = fileUrl.Trim();

            _eVCDB.SaveChanges();
        }

        public void DeleteAllMovies()
        {
            var movies = GetAllMovies();
            _eVCDB.RemoveRange(movies);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleMovie(int movieId)
        {
            var movie = GetSingleMovie(movieId);
            _eVCDB.Remove(movie);
            _eVCDB.SaveChanges();
        }

        public Movie GetSingleMovie(int movieId)
        {
            return _eVCDB.Movies.Single(movie => movie.MovieId == movieId);
        }

        public List<Movie> GetAllMovies(int cinemaId = 0)
        {
            if (cinemaId == 0)
                return _eVCDB.Movies.ToList();
            else
                return _eVCDB.Movies.Where(movie => movie.CinemaId == cinemaId).ToList();
        }

        public dynamic GetAllMoviesDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var movies = GetAllMovies();
            var recordsTotal = movies.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                movies = movies.Where(movie =>
                    movie.Title.ToLower().Contains(searchValue.ToLower()) ||
                    movie.Type.ToLower().Contains(searchValue.ToLower()) ||
                    movie.Actors.ToLower().Contains(searchValue.ToLower())
                ).ToList();
            }

            movies = movies.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var movie in movies)
            {
                data.Add(new object[]
                {
                    movie.MovieId,
                    movie.Title ?? "",
                    movie.Type ?? "",
                    movie.Price,
                    movie.Duration,
                    movie.Description ?? "",
                    movie.Actors ?? "",
                    "<img src=" + movie.Image + " class='img-thumbnail'>",
                    movie.Cinema?.Name ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateMovieGet(" + movie.MovieId + ")'>Update Movie</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllMovies(" + movie.MovieId + ")'>Delete Movie</button>"
                });
            }

            var json = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                data = data
            };

            return json;
        }             
    }
}
