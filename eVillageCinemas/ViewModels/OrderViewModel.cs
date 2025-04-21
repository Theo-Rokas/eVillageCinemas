using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillageCinemas.ViewModels
{
    public class OrderViewModel
    {
        public Cinema? Cinema { get; set; }

        public List<Cinema>? CinemaList { get; set; }

        public Movie? Movie { get; set; }

        public List<Movie>? MovieList { get; set; }

        public AvailableDate? AvailableDate { get; set; }

        public List<AvailableDate>? AvailableDateList { get; set; }

        public List<AvailableSeat>? AvailableSeats { get; set; }

        public List<AvailableSeat>? AvailableSeatList { get; set; }

        public Order? Order { get; set; }

        public List<Order>? OrderList { get; set; }

        public Ticket? Ticket { get; set; }

        public List<Ticket>? TicketList { get; set; }

        public void InitCinema(ICinemaService cinemaService, int cinemaId)
        {
            Cinema = cinemaId == 0 ? new Cinema() { CinemaId = 0, Name = "" } : cinemaService.GetSingleCinema(cinemaId);
        }

        public void InitCinemaList(ICinemaService cinemaService)
        {
            CinemaList = cinemaService.GetAllCinemas();
        }

        public void InitMovie(IMovieService movieService, int movieId)
        {
            Movie = movieId == 0 ? new Movie() { MovieId = 0, Title = "", Type = "", Price = 0, Duration = 0, Description = "", Actors = "", Image = "", CinemaId = 0 } : movieService.GetSingleMovie(movieId);
        }

        public void InitMovieList(IMovieService movieService, int cinemaId = 0)
        {
            MovieList = movieService.GetAllMovies(cinemaId);
        }       

        public void InitAvailableDate(IAvailableDateService availableDateService, int availableDateId)
        {
            AvailableDate = availableDateId == 0 ? new AvailableDate() { AvailableDateId = 0, MovieId = 0, HallId = 0 } : availableDateService.GetSingleAvailableDate(availableDateId);
        }

        public void InitAvailableDateList(IAvailableDateService availableDateService, int movieId = 0, int hallId = 0)
        {
            AvailableDateList = availableDateService.GetAllAvailableDates(movieId, hallId);
        }

        public void InitAvailableSeats(IAvailableSeatService availableSeatService, List<int> availableSeatIds)
        {
            AvailableSeats = !availableSeatIds.Any() ? new List<AvailableSeat>() : availableSeatService.GetSelectedAvailableSeats(availableSeatIds);
        }

        public void InitAvailableSeatList(IAvailableSeatService availableSeatService, int availableDateId)
        {
            AvailableSeatList = availableSeatService.GetAllAvailableSeats(availableDateId);
        }

        public void InitOrder(IOrderService orderService, int orderId)
        {
            Order = orderId == 0 ? new Order() { OrderId = 0, FirstName = "", LastName = "", Email = "", QRCode = "", CheckIn = false } : orderService.GetSingleOrder(orderId);
        }

        public void InitOrderList(IOrderService orderService)
        {
            OrderList = orderService.GetAllOrders();
        }

        public void InitTicket(ITicketService ticketService, int ticketId)
        {
            Ticket = ticketId == 0 ? new Ticket() { TicketId = 0, OrderId = 0, AvailableSeatId = 0 } : ticketService.GetSingleTicket(ticketId);
        }

        public void InitTicketList(ITicketService ticketService, int orderId)
        {
            TicketList = ticketService.GetAllTickets(orderId);
        }
    }
}
