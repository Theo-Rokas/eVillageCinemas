using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface IHallService
    {
        public void CreateHall(AdminViewModel adminViewModel);

        public void UpdateHall(AdminViewModel adminViewModel);

        public void DeleteSingleHall(int hallId);

        public void DeleteAllHalls();

        public Hall GetSingleHall(int hallId);

        public List<Hall> GetAllHalls();

        public dynamic GetAllHallsDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
