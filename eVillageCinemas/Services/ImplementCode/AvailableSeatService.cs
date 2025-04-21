using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class AvailableSeatService : IAvailableSeatService
    {
        private readonly DatabaseContext _eVCDB;

        public AvailableSeatService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }        

        public void CreateAvailableSeats(AdminViewModel adminViewModel)
        {
            var availableDate = _eVCDB.AvailableDates.Single(availableDate => availableDate.AvailableDateId == adminViewModel.AvailableDate.AvailableDateId);

            foreach(var seat in availableDate.Hall.Seats.ToList())
            {
                var availableSeat = new AvailableSeat()
                {
                    Code = seat.Code,
                    IsAvailable = true,
                    AvailableDateId = adminViewModel.AvailableDate.AvailableDateId,
                };

                _eVCDB.AvailableSeats.Add(availableSeat);
            }
            _eVCDB.SaveChanges();
        }  

        public void DeleteAllAvailableSeats()
        {
            var availableSeats = GetAllAvailableSeats();
            _eVCDB.RemoveRange(availableSeats);
            _eVCDB.SaveChanges();
        }

        public List<AvailableSeat> GetAllAvailableSeats(int availableDateId = 0)
        {
            if (availableDateId == 0)
                return _eVCDB.AvailableSeats.ToList();
            else
                return _eVCDB.AvailableSeats.Where(availableSeat => availableSeat.AvailableDateId == availableDateId).ToList();
        }

        public List<AvailableSeat> GetSelectedAvailableSeats(List<int>? availableSeatIds = null)
        {
            availableSeatIds ??= new List<int>();

            if(availableSeatIds.Any())
                return _eVCDB.AvailableSeats.Where(availableSeat => availableSeatIds.Contains(availableSeat.AvailableSeatId)).ToList();
            else
                return _eVCDB.AvailableSeats.ToList();
        }

        public dynamic GetAllAvailableSeatsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var availableSeats = GetAllAvailableSeats();
            var recordsTotal = availableSeats.Count;

            availableSeats = availableSeats.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var availableSeat in availableSeats)
            {
                data.Add(new object[]
                {
                    availableSeat.AvailableSeatId,
                    availableSeat.Code ?? "",
                    availableSeat.IsAvailable,
                    availableSeat.AvailableDate.Date.ToString("dd/MM/yyyy HH:mm") ?? ""
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
