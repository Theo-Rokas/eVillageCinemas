using Amazon.S3.Model;
using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class AvailableDateService : IAvailableDateService
    {
        private readonly DatabaseContext _eVCDB;

        public AvailableDateService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }        

        public void CreateAvailableDate(AdminViewModel adminViewModel)
        {
            for (int i = 0; i < adminViewModel.MultipleNumber; i++)
            {
                var availableDate = new AvailableDate()
                {
                    Date = adminViewModel.AvailableDate.Date,
                    MovieId = adminViewModel.Movie.MovieId,
                    HallId = adminViewModel.Hall.HallId
                };

                _eVCDB.AvailableDates.Add(availableDate);
            }
            _eVCDB.SaveChanges();
        }        

        public void UpdateAvailableDate(AdminViewModel adminViewModel)
        {
            var availableDate = GetSingleAvailableDate(adminViewModel.AvailableDate.AvailableDateId);
            availableDate.Date = adminViewModel.AvailableDate.Date;
            availableDate.MovieId = adminViewModel.Movie.MovieId;
            availableDate.HallId = adminViewModel.Hall.HallId;
            _eVCDB.SaveChanges();
        }

        public void DeleteAllAvailableDates()
        {
            var availableDates = GetAllAvailableDates();
            _eVCDB.RemoveRange(availableDates);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleAvailableDate(int availableDateId)
        {
            var availableDate = GetSingleAvailableDate(availableDateId);
            _eVCDB.Remove(availableDate);
            _eVCDB.SaveChanges();
        }

        public AvailableDate GetSingleAvailableDate(int availableDateId)
        {
            return _eVCDB.AvailableDates.Single(availableDate => availableDate.AvailableDateId == availableDateId);
        }

        public List<AvailableDate> GetAllAvailableDates(int movieId = 0, int hallId = 0)
        {
            var availableDates = _eVCDB.AvailableDates.AsQueryable();

            if (movieId != 0)
                availableDates = availableDates.Where(availableDate => availableDate.MovieId == movieId);

            if (hallId != 0)
                availableDates = availableDates.Where(availableDate => availableDate.HallId == hallId);

            return availableDates.ToList();
        }

        public dynamic GetAllAvailableDatesDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var availableDates = GetAllAvailableDates();
            var recordsTotal = availableDates.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                availableDates = availableDates.Where(availableDate => 
                    availableDate.Movie.Title.ToLower().Contains(searchValue.ToLower()) ||
                    availableDate.Hall.Name.ToLower().Contains(searchValue.ToLower())
                ).ToList();
            }

            availableDates = availableDates.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var availableDate in availableDates)
            {
                data.Add(new object[]
                {
                    availableDate.AvailableDateId,
                    availableDate.Date.ToString("dd/MM/yyyy HH:mm"),
                    availableDate.Movie.Title ?? "",
                    availableDate.Hall.Name ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateAvailableDateGet(" + availableDate.AvailableDateId + ")'>Update Available Date</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllAvailableDates(" + availableDate.AvailableDateId + ")'>Delete Available Date</button>"
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
