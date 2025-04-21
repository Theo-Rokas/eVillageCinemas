using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface IAvailableSeatService
    {
        public void CreateAvailableSeats(AdminViewModel adminViewModel);

        public void DeleteAllAvailableSeats();

        public List<AvailableSeat> GetAllAvailableSeats(int availableDateId = 0);

        public List<AvailableSeat> GetSelectedAvailableSeats(List<int>? availableSeatIds = null);

        public dynamic GetAllAvailableSeatsDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
