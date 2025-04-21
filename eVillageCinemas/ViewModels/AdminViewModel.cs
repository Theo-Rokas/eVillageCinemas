using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace eVillageCinemas.ViewModels
{
    public class AdminViewModel
    {
        public int MultipleNumber { get; set; }

        public Cinema? Cinema { get; set; }

        public List<SelectListItem>? CinemaSelectList { get; set; }

        public Movie? Movie { get; set; }

        public List<SelectListItem>? MovieSelectList { get; set; }

        public Hall? Hall { get; set; }

        public List<SelectListItem>? HallSelectList { get; set; }

        public Seat? Seat { get; set; }

        public List<SelectListItem>? SeatSelectList { get; set; }

        public AvailableDate? AvailableDate { get; set; }

        public List<SelectListItem>? AvailableDateSelectList { get; set; }

        public List<AvailableSeat>? AvailableSeats { get; set; }

        public List<SelectListItem>? AvailableSeatSelectList { get; set; }

        public Order? Order { get; set; }

        public List<SelectListItem>? OrderSelectList { get; set; }

        public Ticket? Ticket { get; set; }

        public List<SelectListItem>? TicketSelectList { get; set; }

        public IFormFile? File { get; set; }

        public AdminViewModel() 
        {
            MultipleNumber = 1;
        }

        public void InitCinema(ICinemaService cinemaService, int cinemaId)
        {
            Cinema = cinemaId == 0 ? new Cinema() { CinemaId = 0, Name = ""} : cinemaService.GetSingleCinema(cinemaId);
        }

        public void InitCinemaSelectList(ICinemaService cinemaService)
        {
            var cinemaSelectList = new List<SelectListItem>();
            var cinemas = cinemaService.GetAllCinemas();

            foreach(var cinema in cinemas)
            {
                cinemaSelectList.Add(new SelectListItem { Value = cinema.CinemaId.ToString(), Text = cinema.Name });
            }
            
            CinemaSelectList = cinemaSelectList;
        }

        public void InitMovie(IMovieService movieService, int movieId)
        {
            Movie = movieId == 0 ? new Movie() { MovieId = 0, Title = "", Type = "", Price = 0, Duration = 0, Description = "", Actors = "", Image = "", CinemaId = 0 } : movieService.GetSingleMovie(movieId);
        }

        public void InitMovieSelectList(IMovieService movieService)
        {
            var movieSelectList = new List<SelectListItem>();
            var movies = movieService.GetAllMovies();

            foreach (var movie in movies)
            {
                movieSelectList.Add(new SelectListItem { Value = movie.MovieId.ToString(), Text = movie.Title });
            }

            MovieSelectList = movieSelectList;
        }

        public void InitHall(IHallService hallService, int hallId)
        {
            Hall = hallId == 0 ? new Hall() { HallId = 0, Name = "" } : hallService.GetSingleHall(hallId);
        }

        public void InitHallSelectList(IHallService hallService)
        {
            var hallSelectList = new List<SelectListItem>();
            var halls = hallService.GetAllHalls();

            foreach (var hall in halls)
            {
                hallSelectList.Add(new SelectListItem { Value = hall.HallId.ToString(), Text = hall.Name });
            }

            HallSelectList = hallSelectList;
        }

        public void InitSeat(ISeatService seatService, int seatId)
        {
            Seat = seatId == 0 ? new Seat() { SeatId = 0, Code = "" } : seatService.GetSingleSeat(seatId);
        }

        public void InitSeatSelectList(ISeatService seatService)
        {
            var seatSelectList = new List<SelectListItem>();
            var seats = seatService.GetAllSeats();

            foreach (var seat in seats)
            {
                seatSelectList.Add(new SelectListItem { Value = seat.SeatId.ToString(), Text = seat.Code });
            }

            SeatSelectList = seatSelectList;
        }

        public void InitAvailableDate(IAvailableDateService availableDateService, int availableDateId)
        {
            AvailableDate = availableDateId == 0 ? new AvailableDate() { AvailableDateId = 0, MovieId = 0, HallId = 0 } : availableDateService.GetSingleAvailableDate(availableDateId);
        }

        public void InitAvailableDateSelectList(IAvailableDateService availableDateService)
        {
            var availableDateSelectList = new List<SelectListItem>();
            var availableDates = availableDateService.GetAllAvailableDates();

            foreach (var availableDate in availableDates)
            {
                availableDateSelectList.Add(new SelectListItem { Value = availableDate.AvailableDateId.ToString(), Text = availableDate.Date.ToString("dd/MM/yyyy HH:mm") });
            }

            AvailableDateSelectList = availableDateSelectList;
        }

        public void InitAvailableSeats(IAvailableSeatService availableSeatService, List<int> availableSeatIds)
        {
            AvailableSeats = !availableSeatIds.Any() ? new List<AvailableSeat>() : availableSeatService.GetSelectedAvailableSeats(availableSeatIds);
        }

        public void InitAvailableSeatSelectList(IAvailableSeatService availableSeatService)
        {
            var availableSeatSelectList = new List<SelectListItem>();
            var availableSeats = availableSeatService.GetSelectedAvailableSeats();

            foreach (var availableSeat in availableSeats)
            {
                availableSeatSelectList.Add(new SelectListItem { Value = availableSeat.AvailableSeatId.ToString(), Text = "" });
            }

            AvailableSeatSelectList = availableSeatSelectList;
        }
    }
}
