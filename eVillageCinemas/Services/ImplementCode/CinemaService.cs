using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class CinemaService : ICinemaService
    {
        private readonly DatabaseContext _eVCDB;

        public CinemaService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }

        public void CreateCinema(AdminViewModel adminViewModel)
        {
            for (int i = 0; i < adminViewModel.MultipleNumber; i++)
            {
                var cinema = new Cinema()
                {
                    Name = adminViewModel.Cinema?.Name?.Trim()
                };

                _eVCDB.Cinemas.Add(cinema);
            }
            _eVCDB.SaveChanges();
        }

        public void UpdateCinema(AdminViewModel adminViewModel)
        {
            var cinema = GetSingleCinema(adminViewModel.Cinema.CinemaId);
            cinema.Name = adminViewModel.Cinema?.Name?.Trim();
            _eVCDB.SaveChanges();
        }

        public void DeleteAllCinemas()
        {
            var cinemas = GetAllCinemas();
            _eVCDB.RemoveRange(cinemas);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleCinema(int cinemaId)
        {
            var cinema = GetSingleCinema(cinemaId);
            _eVCDB.Remove(cinema);
            _eVCDB.SaveChanges();
        }

        public Cinema GetSingleCinema(int cinemaId)
        {
            return _eVCDB.Cinemas.Single(cinema => cinema.CinemaId == cinemaId);
        }

        public List<Cinema> GetAllCinemas()
        {
            return _eVCDB.Cinemas.ToList();
        }

        public dynamic GetAllCinemasDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var cinemas = GetAllCinemas();
            var recordsTotal = cinemas.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                cinemas = cinemas.Where(cinema => cinema.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            cinemas = cinemas.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var cinema in cinemas)
            {
                data.Add(new object[]
                {
                    cinema.CinemaId,
                    cinema.Name ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateCinemaGet(" + cinema.CinemaId + ")'>Update Cinema</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllCinemas(" + cinema.CinemaId + ")'>Delete Cinema</button>"
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
