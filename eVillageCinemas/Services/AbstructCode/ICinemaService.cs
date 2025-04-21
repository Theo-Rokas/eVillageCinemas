using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface ICinemaService
    {
        public void CreateCinema(AdminViewModel adminViewModel);

        public void UpdateCinema(AdminViewModel adminViewModel);

        public void DeleteSingleCinema(int cinemaId);

        public void DeleteAllCinemas();

        public Cinema GetSingleCinema(int cinemaId);

        public List<Cinema> GetAllCinemas();

        public dynamic GetAllCinemasDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
