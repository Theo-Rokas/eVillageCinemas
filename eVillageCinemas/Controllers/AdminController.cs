using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eVillageCinemas.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;
        private readonly ISeatService _seatService;
        private readonly IAvailableDateService _availableDateService;
        private readonly IAvailableSeatService _availableSeatService;        

        public AdminController(
            ICinemaService cinemaService,
            IMovieService movieService,
            IHallService hallService,
            ISeatService seatService,
            IAvailableDateService availableDateService, 
            IAvailableSeatService availableSeatService
            
        )
        {
            _cinemaService = cinemaService;
            _movieService = movieService;
            _hallService = hallService;
            _seatService = seatService;
            _availableDateService = availableDateService;
            _availableSeatService = availableSeatService;            
        }

        #region Cinema  
        public IActionResult Cinemas()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult CinemasDatatable(int draw, int start, int length, Dictionary<string, string> search) 
        {
            return Json(_cinemaService.GetAllCinemasDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateOrUpdateCinema(int cinemaId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitCinema(_cinemaService, cinemaId);
            return PartialView("~/Views/Admin/PartialViews/_CinemaForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateCinema(AdminViewModel adminViewModel)
        {
            if(adminViewModel.Cinema?.CinemaId == 0)
            {
                _cinemaService.CreateCinema(adminViewModel);
            }
            else
            {
                _cinemaService.UpdateCinema(adminViewModel);
            }

            return Content("OK");
        }

        [HttpGet] 
        public IActionResult DeleteSingleOrAllCinemas(int cinemaId = 0)
        {
            if(cinemaId == 0)
            {
                _cinemaService.DeleteAllCinemas();
            }
            else
            {
                _cinemaService.DeleteSingleCinema(cinemaId);
            }

            return Content("OK");
        }
        #endregion

        #region Movie  
        public IActionResult Movies()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult MoviesDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            return Json(_movieService.GetAllMoviesDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateOrUpdateMovie(int movieId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitMovie(_movieService, movieId);
            adminViewModel.InitCinemaSelectList(_cinemaService);
            return PartialView("~/Views/Admin/PartialViews/_MovieForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateMovie(AdminViewModel adminViewModel)
        {
            if (adminViewModel.Movie?.MovieId == 0)
            {
                _movieService.CreateMovie(adminViewModel);
            }
            else
            {
                _movieService.UpdateMovie(adminViewModel);
            }

            return Content("OK");
        }

        [HttpGet]
        public IActionResult DeleteSingleOrAllMovies(int movieId = 0)
        {
            if (movieId == 0)
            {
                _movieService.DeleteAllMovies();
            }
            else
            {
                _movieService.DeleteSingleMovie(movieId);
            }

            return Content("OK");
        }
        #endregion

        #region Hall  
        public IActionResult Halls()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult HallsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            return Json(_hallService.GetAllHallsDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateOrUpdateHall(int hallId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitHall(_hallService, hallId);
            adminViewModel.InitMovieSelectList(_movieService);
            return PartialView("~/Views/Admin/PartialViews/_HallForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateHall(AdminViewModel adminViewModel)
        {
            if (adminViewModel.Hall?.HallId == 0)
            {
                _hallService.CreateHall(adminViewModel);
            }
            else
            {
                _hallService.UpdateHall(adminViewModel);
            }

            return Content("OK");
        }

        [HttpGet]
        public IActionResult DeleteSingleOrAllHalls(int hallId = 0)
        {
            if (hallId == 0)
            {
                _hallService.DeleteAllHalls();
            }
            else
            {
                _hallService.DeleteSingleHall(hallId);
            }

            return Content("OK");
        }
        #endregion

        #region Seat  
        public IActionResult Seats()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult SeatsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            return Json(_seatService.GetAllSeatsDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateOrUpdateSeat(int seatId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitSeat(_seatService, seatId);
            adminViewModel.InitHallSelectList(_hallService);
            return PartialView("~/Views/Admin/PartialViews/_SeatForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateSeat(AdminViewModel adminViewModel)
        {
            if (adminViewModel.Seat?.SeatId == 0)
            {
                _seatService.CreateSeat(adminViewModel);
            }
            else
            {
                _seatService.UpdateSeat(adminViewModel);
            }

            return Content("OK");
        }

        [HttpGet]
        public IActionResult DeleteSingleOrAllSeats(int seatId = 0)
        {
            if (seatId == 0)
            {
                _seatService.DeleteAllSeats();
            }
            else
            {
                _seatService.DeleteSingleSeat(seatId);
            }

            return Content("OK");
        }
        #endregion

        #region AvailableDate  
        public IActionResult AvailableDates()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult AvailableDatesDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            return Json(_availableDateService.GetAllAvailableDatesDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateOrUpdateAvailableDate(int availableDateId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitAvailableDate(_availableDateService, availableDateId);
            adminViewModel.InitMovieSelectList(_movieService);
            adminViewModel.InitHallSelectList(_hallService);
            return PartialView("~/Views/Admin/PartialViews/_AvailableDateForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateAvailableDate(AdminViewModel adminViewModel)
        {
            if (adminViewModel.AvailableDate?.AvailableDateId == 0)
            {
                _availableDateService.CreateAvailableDate(adminViewModel);
            }
            else
            {
                _availableDateService.UpdateAvailableDate(adminViewModel);
            }

            return Content("OK");
        }

        [HttpGet]
        public IActionResult DeleteSingleOrAllAvailableDates(int availableDateId = 0)
        {
            if (availableDateId == 0)
            {
                _availableDateService.DeleteAllAvailableDates();
            }
            else
            {
                _availableDateService.DeleteSingleAvailableDate(availableDateId);
            }

            return Content("OK");
        }
        #endregion

        #region AvailableSeat  
        public IActionResult AvailableSeats()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult AvailableSeatsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            return Json(_availableSeatService.GetAllAvailableSeatsDatatable(draw, start, length, search));
        }

        [HttpGet]
        public IActionResult CreateAvailableSeats()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.InitAvailableSeats(_availableSeatService, new List<int>());
            adminViewModel.InitAvailableDateSelectList(_availableDateService);
            return PartialView("~/Views/Admin/PartialViews/_AvailableSeatForm.cshtml", adminViewModel);
        }

        [HttpPost]
        public IActionResult CreateAvailableSeats(AdminViewModel adminViewModel)
        {            
            _availableSeatService.CreateAvailableSeats(adminViewModel);

            return Content("OK");
        }

        [HttpGet]
        public IActionResult DeleteAllAvailableSeats()
        {
            _availableSeatService.DeleteAllAvailableSeats();

            return Content("OK");
        }
        #endregion
    }
}
