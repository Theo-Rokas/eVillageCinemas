using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface IAvailableDateService
    {
        public void CreateAvailableDate(AdminViewModel adminViewModel);

        public void UpdateAvailableDate(AdminViewModel adminViewModel);

        public void DeleteSingleAvailableDate(int availableDateId);

        public void DeleteAllAvailableDates();

        public AvailableDate GetSingleAvailableDate(int availableDateId);

        public List<AvailableDate> GetAllAvailableDates(int movieId = 0, int hallId = 0);

        public dynamic GetAllAvailableDatesDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
