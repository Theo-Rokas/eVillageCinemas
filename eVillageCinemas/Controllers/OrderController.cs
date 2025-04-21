using Amazon.S3.Model;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eVillageCinemas.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        private readonly IAvailableDateService _availableDateService;
        private readonly IAvailableSeatService _availableSeatService;
        private readonly IOrderService _orderService;
        private readonly ITicketService _ticketService;
        private readonly IHelperService _helperService;
        
        public OrderController(
            ICinemaService cinemaService,
            IMovieService movieService,
            IAvailableDateService availableDateService,
            IAvailableSeatService availableSeatService,
            IOrderService orderService,
            ITicketService ticketService,
            IHelperService helperService
        )
        {
            _cinemaService = cinemaService;
            _movieService = movieService;
            _availableDateService = availableDateService;
            _availableSeatService = availableSeatService;
            _orderService = orderService;
            _ticketService = ticketService;
            _helperService = helperService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            OrderViewModel clientViewModel = new OrderViewModel();            
            clientViewModel.InitCinemaList(_cinemaService);
            return View(clientViewModel);
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel clientViewModel)
        {
            var orderId = _orderService.CreateOrder(clientViewModel);
            clientViewModel.InitOrder(_orderService, orderId);
            _ticketService.CreateTicket(clientViewModel);

            return Json(new
            {
                status = "OK",
                redirectUrl = Url.Action("Create", "Payment", new { orderId = orderId })
            });
        }

        public IActionResult LoadMovies(int cinemaId)
        {
            OrderViewModel clientViewModel = new OrderViewModel();
            clientViewModel.InitCinema(_cinemaService, cinemaId);
            clientViewModel.InitMovieList(_movieService, cinemaId);
            return PartialView("~/Views/Order/PartialViews/_Movies.cshtml", clientViewModel);
        }

        public IActionResult LoadAvailableDates(int movieId)
        {
            OrderViewModel clientViewModel = new OrderViewModel();
            clientViewModel.InitMovie(_movieService, movieId);
            clientViewModel.InitAvailableDateList(_availableDateService, movieId);
            return PartialView("~/Views/Order/PartialViews/_AvailableDates.cshtml", clientViewModel);
        }

        public IActionResult LoadAvailableSeats(int availableDateId)
        {
            OrderViewModel clientViewModel = new OrderViewModel();
            clientViewModel.InitAvailableDate(_availableDateService, availableDateId);
            clientViewModel.InitAvailableSeatList(_availableSeatService, availableDateId);
            return PartialView("~/Views/Order/PartialViews/_AvailableSeats.cshtml", clientViewModel);
        }

        public IActionResult LoadOrderForm(string selectedSeats)
        {
            var availableSeatIds = new List<int>();
            
            if(!string.IsNullOrWhiteSpace(selectedSeats))
            {
                foreach(var selectedSeat in selectedSeats.Split(','))
                {
                    var availableSeatId = int.Parse(selectedSeat);
                    availableSeatIds.Add(availableSeatId);
                }
            }

            OrderViewModel clientViewModel = new OrderViewModel();
            clientViewModel.InitAvailableSeats(_availableSeatService, availableSeatIds);
            clientViewModel.InitOrder(_orderService, 0);
            return PartialView("~/Views/Order/PartialViews/_OrderForm.cshtml", clientViewModel);
        }

        [HttpGet]
        public IActionResult CheckIn(int orderId) 
        {
            var message = _orderService.CheckInOrder(orderId);
            return Json(new { status = "OK", message });
        }
    }
}
