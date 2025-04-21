using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface ISeatService
    {
        public void CreateSeat(AdminViewModel adminViewModel);

        public void UpdateSeat(AdminViewModel adminViewModel);

        public void DeleteSingleSeat(int seatId);

        public void DeleteAllSeats();

        public Seat GetSingleSeat(int seatId);

        public List<Seat> GetAllSeats(int hallId = 0);

        public dynamic GetAllSeatsDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
