using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class HallService : IHallService
    {
        private readonly DatabaseContext _eVCDB;

        public HallService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }

        public void CreateHall(AdminViewModel adminViewModel)
        {
            for (int i = 0; i < adminViewModel.MultipleNumber; i++)
            {
                var hall = new Hall()
                {
                    Name = adminViewModel.Hall?.Name?.Trim()
                };

                _eVCDB.Halls.Add(hall);
            }
            _eVCDB.SaveChanges();
        }

        public void UpdateHall(AdminViewModel adminViewModel)
        {
            var hall = GetSingleHall(adminViewModel.Hall.HallId);
            hall.Name = adminViewModel.Hall?.Name?.Trim();
            _eVCDB.SaveChanges();
        }

        public void DeleteAllHalls()
        {
            var halls = GetAllHalls();
            _eVCDB.RemoveRange(halls);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleHall(int hallId)
        {
            var hall = GetSingleHall(hallId);
            _eVCDB.Remove(hall);
            _eVCDB.SaveChanges();
        }

        public Hall GetSingleHall(int hallId)
        {
            return _eVCDB.Halls.Single(hall => hall.HallId == hallId);
        }

        public List<Hall> GetAllHalls()
        {
            return _eVCDB.Halls.ToList();
        }

        public dynamic GetAllHallsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var halls = GetAllHalls();
            var recordsTotal = halls.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                halls = halls.Where(hall => hall.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            halls = halls.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var hall in halls)
            {
                data.Add(new object[]
                {
                    hall.HallId,
                    hall.Name ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateHallGet(" + hall.HallId + ")'>Update Hall</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllHalls(" + hall.HallId + ")'>Delete Hall</button>"
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
